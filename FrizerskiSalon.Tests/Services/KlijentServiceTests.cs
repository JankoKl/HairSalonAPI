using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Entities;
using FrizerskiSalon.API.Services;
using FrizerskiSalon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FrizerskiSalon.Tests.Services;

public class KlijentServiceTests
{
    private FrizerskiSalonContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<FrizerskiSalonContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new FrizerskiSalonContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CreateAsync_ShouldAddKlijent()
    {
        var context = GetDbContext();
        var service = new KlijentService(context);

        var klijent = new Klijent { Ime = "Ana", Prezime = "Anić",  BrTel = "12345" };

        var result = await service.CreateAsync(klijent);

        Assert.NotNull(result);
        Assert.Equal("Ana", result.Ime);
        Assert.Single(context.Klijenti);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllKlijenti()
    {
        var context = GetDbContext();
        context.Klijenti.AddRange(
            new Klijent { Ime = "A", Prezime = "A", BrTel = "1" },
            new Klijent { Ime = "B", Prezime = "B", BrTel = "2" });
        await context.SaveChangesAsync();

        var service = new KlijentService(context);
        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveKlijent()
    {
        var context = GetDbContext();
        var klijent = new Klijent { Ime = "X", Prezime = "Y", BrTel = "000" };
        context.Klijenti.Add(klijent);
        await context.SaveChangesAsync();

        var service = new KlijentService(context);
        var deleted = await service.DeleteAsync(klijent.Id);

        Assert.True(deleted);
        Assert.Empty(context.Klijenti);
    }

    [Fact]
    public async Task GetUkupnoTerminaAsync_ShouldReturnCorrectCount()
    {
        var context = GetDbContext();
        var klijent = new Klijent { Ime = "K", Prezime = "L",  BrTel = "555" };
        var radnik = new Radnik { Ime = "R", Prezime = "S", Pozicija = "Frizer", BrTel = "111" };

        context.Klijenti.Add(klijent);
        context.Radnici.Add(radnik);
        await context.SaveChangesAsync();

        context.Termini.AddRange(
            new Termin { KlijentID = klijent.Id, RadnikID = radnik.Id, Datum = DateTime.Today, UslugaID = 1 },
            new Termin { KlijentID = klijent.Id, RadnikID = radnik.Id, Datum = DateTime.Today, UslugaID = 1 });

        await context.SaveChangesAsync();

        var service = new KlijentService(context);
        var count = await service.GetUkupnoTerminaAsync(klijent.Id);

        Assert.Equal(2, count);
    }
}
