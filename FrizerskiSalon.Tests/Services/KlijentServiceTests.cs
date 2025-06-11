using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Entities;
using FrizerskiSalon.API.Services;
using System.Linq;

namespace FrizerskiSalon.Tests.Services
{
    public class KlijentServiceTests
    {
        private FrizerskiSalonContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<FrizerskiSalonContext>()
                .UseInMemoryDatabase($"_KlijentDb_{Guid.NewGuid()}")
                .Options;
            return new FrizerskiSalonContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddKlijent_WhenAllDataIsValid()
        {
            using var context = CreateDbContext();
            var service = new KlijentService(context);

            var klijent = new Klijent
            {
                Ime = "Test",
                Prezime = "User",
                BrTel = "+38161123456"
            };

            var result = await service.CreateAsync(klijent);
            var stored = await context.Klijenti.SingleAsync();

            Assert.NotNull(result);
            Assert.Equal("Test", stored.Ime);
            Assert.Equal("User", stored.Prezime);
            Assert.Equal("+38161123456", stored.BrTel);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllKlijenti()
        {
            using var context = CreateDbContext();
            context.Klijenti.AddRange(
                new Klijent { Ime = "A", Prezime = "A", BrTel = "1" },
                new Klijent { Ime = "B", Prezime = "B", BrTel = "2" }
            );
            await context.SaveChangesAsync();

            var service = new KlijentService(context);

            var all = await service.GetAllAsync();
            Assert.Equal(2, all.Count());
            Assert.All(all, k => Assert.False(string.IsNullOrWhiteSpace(k.Ime)));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectKlijent_OrNull()
        {
            using var context = CreateDbContext();
            var klijent = new Klijent { Ime = "Ana", Prezime = "P", BrTel = "123" };
            context.Klijenti.Add(klijent);
            await context.SaveChangesAsync();

            var service = new KlijentService(context);

            var fetched = await service.GetByIdAsync(klijent.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Ana", fetched.Ime);

            var notFound = await service.GetByIdAsync(-42);
            Assert.Null(notFound);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateKlijent_WhenExists()
        {
            using var context = CreateDbContext();
            var klijent = new Klijent { Ime = "Old", Prezime = "Data", BrTel = "1" };
            context.Klijenti.Add(klijent);
            await context.SaveChangesAsync();

            var updated = new Klijent
            {
                Id = klijent.Id,
                Ime = "New",
                Prezime = "Name",
                BrTel = "2222"
            };
            var service = new KlijentService(context);

            var success = await service.UpdateAsync(updated);
            var stored = await context.Klijenti.SingleAsync();

            Assert.True(success);
            Assert.Equal("New", stored.Ime);
            Assert.Equal("2222", stored.BrTel);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_IfKlijentDoesNotExist()
        {
            using var context = CreateDbContext();
            var service = new KlijentService(context);

            var missing = new Klijent { Id = 777, Ime = "X", Prezime = "Y", BrTel = "Z" };
            var updated = await service.UpdateAsync(missing);

            Assert.False(updated);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveKlijent_IfExists()
        {
            using var context = CreateDbContext();
            var klijent = new Klijent { Ime = "Will", Prezime = "BeDeleted", BrTel = "1000" };
            context.Klijenti.Add(klijent);
            await context.SaveChangesAsync();

            var service = new KlijentService(context);
            var deleted = await service.DeleteAsync(klijent.Id);

            Assert.True(deleted);
            Assert.Empty(context.Klijenti);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_IfKlijentNotFound()
        {
            using var context = CreateDbContext();
            var service = new KlijentService(context);

            var deleted = await service.DeleteAsync(-123);
            Assert.False(deleted);
        }

        [Fact]
        public async Task GetUkupnoTerminaAsync_ShouldReturnCountForThatKlijent()
        {
            using var context = CreateDbContext();
            // Setup Klijent
            var klijent = new Klijent { Ime = "Has", Prezime = "Appointments", BrTel = "09" };
            context.Klijenti.Add(klijent);
            await context.SaveChangesAsync();

            // Setup another Klijent & Radnik
            var other = new Klijent { Ime = "No", Prezime = "Appointments", BrTel = "11" };
            var radnik = new Radnik { Ime = "R", Prezime = "S", Pozicija = "Frizer", BrTel = "111", Email = "r@s.com" };
            context.Klijenti.Add(other);
            context.Radnici.Add(radnik);
            await context.SaveChangesAsync();

            // Two appointments for the first klijent, one for the other
            context.Termini.AddRange(
                new Termin { KlijentID = klijent.Id, RadnikID = radnik.Id, Datum = DateTime.Now, Status = "Zakazano", UslugaID = 0 },
                new Termin { KlijentID = klijent.Id, RadnikID = radnik.Id, Datum = DateTime.Now, Status = "Zakazano", UslugaID = 0 },
                new Termin { KlijentID = other.Id, RadnikID = radnik.Id, Datum = DateTime.Now, Status = "Zakazano", UslugaID = 0 }
            );
            await context.SaveChangesAsync();

            var service = new KlijentService(context);
            var count = await service.GetUkupnoTerminaAsync(klijent.Id);
            Assert.Equal(2, count);

            var none = await service.GetUkupnoTerminaAsync(-99);
            Assert.Equal(0, none);
        }
    }
}
