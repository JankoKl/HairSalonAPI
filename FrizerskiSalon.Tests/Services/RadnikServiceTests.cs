using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FrizerskiSalon.API.Entities;
using FrizerskiSalon.API.Data;

namespace FrizerskiSalon.Tests.Services
{
    public class RadnikServiceTests
    {
        private DbContextOptions<FrizerskiSalonContext> GetInMemoryOptions(string dbName)
        {
            return new DbContextOptionsBuilder<FrizerskiSalonContext>()
                .UseInMemoryDatabase(databaseName: dbName + Guid.NewGuid())
                .Options;
        }

        [Fact]
        public async Task CreateRadnik_WithAllRequiredFields_Succeeds()
        {
            var options = GetInMemoryOptions(nameof(CreateRadnik_WithAllRequiredFields_Succeeds));
            using (var context = new FrizerskiSalonContext(options))
            {
                var radnik = new Radnik
                {
                    Ime = "Milos",
                    Prezime = "Milic",
                    BrTel = "+381661234567",
                    Email = "milos.milic@example.com",
                    Pozicija = "Frizer"
                };
                context.Radnici.Add(radnik);
                await context.SaveChangesAsync();

                Assert.Equal(1, await context.Radnici.CountAsync());
                var saved = await context.Radnici.FirstAsync();
                Assert.Equal("Milos", saved.Ime);
                Assert.Equal("Frizer", saved.Pozicija);
            }
        }

        [Fact]
        public async Task CreateRadnik_MissingRequiredEmail_ThrowsDbUpdateException()
        {
            var options = GetInMemoryOptions(nameof(CreateRadnik_MissingRequiredEmail_ThrowsDbUpdateException));
            using (var context = new FrizerskiSalonContext(options))
            {
                var radnik = new Radnik
                {
                    Ime = "Nemanja",
                    Prezime = "Nikolic",
                    BrTel = "+381661111111",
                    // Email intentionally missing
                    Pozicija = "Pomoćnik"
                };
                context.Radnici.Add(radnik);
                await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
            }
        }

        [Fact]
        public async Task UpdateRadnik_ChangesPozicija_Succeeds()
        {
            var options = GetInMemoryOptions(nameof(UpdateRadnik_ChangesPozicija_Succeeds));
            using (var context = new FrizerskiSalonContext(options))
            {
                var radnik = new Radnik
                {
                    Ime = "Ana",
                    Prezime = "Radic",
                    BrTel = "+38166222333",
                    Email = "ana.radic@example.com",
                    Pozicija = "Pomoćnik"
                };
                context.Radnici.Add(radnik);
                await context.SaveChangesAsync();

                radnik.Pozicija = "Administrator";
                context.Radnici.Update(radnik);
                await context.SaveChangesAsync();

                var updated = await context.Radnici.FirstAsync();
                Assert.Equal("Administrator", updated.Pozicija);
            }
        }

        [Fact]
        public async Task DeleteRadnik_RemovesFromDatabase()
        {
            var options = GetInMemoryOptions(nameof(DeleteRadnik_RemovesFromDatabase));
            using (var context = new FrizerskiSalonContext(options))
            {
                var radnik = new Radnik
                {
                    Ime = "Ivana",
                    Prezime = "Jovic",
                    BrTel = "+38169999999",
                    Email = "ivana.jovic@example.com",
                    Pozicija = "Frizer"
                };
                context.Radnici.Add(radnik);
                await context.SaveChangesAsync();

                context.Radnici.Remove(radnik);
                await context.SaveChangesAsync();

                Assert.Empty(context.Radnici);
            }
        }

        [Fact]
        public async Task CanReadMultipleRadnici()
        {
            var options = GetInMemoryOptions(nameof(CanReadMultipleRadnici));
            using (var context = new FrizerskiSalonContext(options))
            {
                context.Radnici.Add(new Radnik
                {
                    Ime = "Marko", Prezime = "Petrovic", BrTel = "+381640123456", Email = "marko.petrovic@example.com", Pozicija = "Frizer"
                });
                context.Radnici.Add(new Radnik
                {
                    Ime = "Anja", Prezime = "Kovacevic", BrTel = "+381650654321", Email = "anja.kovacevic@example.com", Pozicija = "Recepcioner"
                });
                await context.SaveChangesAsync();

                var allRadnici = await context.Radnici.ToListAsync();
                Assert.Equal(2, allRadnici.Count);
            }
        }
    }
}
