using FrizerskiSalon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrizerskiSalon.API.Data;

public class FrizerskiSalonContext(DbContextOptions<FrizerskiSalonContext> options) 
    : DbContext(options)
{
    public DbSet<Radnik> Radnici { get; set; } = null!;
    public DbSet<Klijent> Klijenti { get; set; } = null!;
    public DbSet<Usluga> Usluge { get; set; } = null!;
    public DbSet<Termin> Termini { get; set; } = null!;
    public DbSet<Izvestaj> Izvestaji { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Defining relationships
        modelBuilder.Entity<Termin>()
            .HasOne(t => t.Radnik)
            .WithMany(z => z.Termini)
            .HasForeignKey(t => t.RadnikID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Termin>()
            .HasOne(t => t.Klijent)
            .WithMany(k => k.Termini)
            .HasForeignKey(t => t.KlijentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Termin>()
            .HasOne(t => t.Usluga)
            .WithMany(u => u.Termini)
            .HasForeignKey(t => t.UslugaID)
            .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<Radnik>().HasData(
            new Radnik { Id = 1, Ime = "Marko", Prezime = "Jovanović", BrTel = "+381641234567", Email = "marko.jovanovic@example.com", Pozicija = "Frizer" },
            new Radnik { Id = 2, Ime = "Ana", Prezime = "Petrović", BrTel = "+381652345678", Email = "ana.petrović@example.com", Pozicija = "Pomoćnik" }
        );

        // Seeding Klijenti (Clients)
        modelBuilder.Entity<Klijent>().HasData(
            new Klijent { Id = 1, Ime = "Miloš", Prezime = "Milić", BrTel = "+3816600001" },
            new Klijent { Id = 2, Ime = "Tatjana", Prezime = "Konstatinović", BrTel = "+38164000770" }
        );

        // Seeding Usluge (Services)
        modelBuilder.Entity<Usluga>().HasData(
            new Usluga { Id = 1, Naziv = "Šišanje", Opis = "Skraćivanje kose", Cena = 600 },
            new Usluga { Id = 2, Naziv = "Brijanje", Opis = "Oblikovanje brade i brkova", Cena = 200 }
        );

        // Seeding Termini (Appointments)
        modelBuilder.Entity<Termin>().HasData(
            new Termin { Id = 1, Datum = new DateTime(2025, 4, 15), Status = "Zakazano", RadnikID = 1, KlijentID = 1, UslugaID = 1 },
            new Termin { Id = 2, Datum = new DateTime(2025, 4, 14), Status = "Zakazano", RadnikID = 2, KlijentID = 2, UslugaID = 2 }
        );

     
    }   
    }

    

