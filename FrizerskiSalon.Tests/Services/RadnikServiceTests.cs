using System.Data;
using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Entities;
using FrizerskiSalon.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FrizerskiSalon.Tests.Services;

public class RadnikServiceTests
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
    public async Task CreateAsync_ShouldAddRadnik()
    {
        // Arrange
        var context = GetDbContext();
        var service = new RadnikService(context);
        var radnik = new Radnik { Ime = "Marko", Prezime = "Marković", Pozicija = "Frizer", BrTel = "123456789" };

        // Act
        var result = await service.CreateAsync(radnik);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Marko", result.Ime);
        Assert.Single(context.Radnici);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllRadnici()
    {
        // Arrange
        var context = GetDbContext();
        context.Radnici.Add(new Radnik { Ime = "Ana", Prezime = "Anić", Pozicija = "Frizer", BrTel = "987654321" });
        context.Radnici.Add(new Radnik { Ime = "Jovan", Prezime = "Jovanović", Pozicija = "Frizer", BrTel = "123123123" });
        await context.SaveChangesAsync();
        var service = new RadnikService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveRadnik()
    {
        // Arrange
        var context = GetDbContext();
        var radnik = new Radnik { Ime = "Ivan", Prezime = "Ivić", Pozicija = "Frizer", BrTel = "1122334455" };
        context.Radnici.Add(radnik);
        await context.SaveChangesAsync();
        var service = new RadnikService(context);

        // Act
        var deleted = await service.DeleteAsync(radnik.Id);

        // Assert
        Assert.True(deleted);
        Assert.Empty(context.Radnici);
    }

    [Fact]
    public async Task GetBrojKlijenataAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var context = GetDbContext();

        var radnik = new Radnik { Ime = "Test", Prezime = "Radnik", Pozicija = "Frizer", BrTel = "000" };
        var klijent1 = new Klijent { Ime = "K1", Prezime = "P1", BrTel = "1" };
        var klijent2 = new Klijent { Ime = "K2", Prezime = "P2",  BrTel = "2" };

        context.Radnici.Add(radnik);
        context.Klijenti.AddRange(klijent1, klijent2);
        await context.SaveChangesAsync();

        context.Termini.AddRange(
            new Termin { RadnikID = radnik.Id, KlijentID = klijent1.Id, Datum = DateTime.Today },
            new Termin { RadnikID = radnik.Id, KlijentID = klijent2.Id, Datum = DateTime.Today },
            new Termin { RadnikID = radnik.Id, KlijentID = klijent1.Id, Datum = DateTime.Today }
        );

        await context.SaveChangesAsync();

        var service = new RadnikService(context);

        // Act
        var brojKlijenata = await service.GetBrojKlijenataAsync(radnik.Id);

        // Assert
        Assert.Equal(2, brojKlijenata); // distinct
    }
}
