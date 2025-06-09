using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Entities;
using FrizerskiSalon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FrizerskiSalon.API.Services;

public class RadnikService : IRadnikService
{
    private readonly FrizerskiSalonContext _context;

    public RadnikService(FrizerskiSalonContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Radnik>> GetAllAsync()
    {
        return await _context.Radnici.ToListAsync();
    }

    public async Task<Radnik?> GetByIdAsync(int id)
    {
        return await _context.Radnici.FindAsync(id);
    }

    public async Task<Radnik> CreateAsync(Radnik radnik)
    {
        _context.Radnici.Add(radnik);
        await _context.SaveChangesAsync();
        return radnik;
    }

    public async Task<Radnik?> UpdateAsync(int id, Radnik updatedRadnik)
    {
        var radnik = await _context.Radnici.FindAsync(id);
        if (radnik == null) return null;

        radnik.Ime = updatedRadnik.Ime;
        radnik.Prezime = updatedRadnik.Prezime;
        radnik.Pozicija = updatedRadnik.Pozicija;
        radnik.BrTel = updatedRadnik.BrTel;

        await _context.SaveChangesAsync();
        return radnik;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var radnik = await _context.Radnici.FindAsync(id);
        if (radnik == null) return false;

        _context.Radnici.Remove(radnik);
        await _context.SaveChangesAsync();
        return true;
    }

      public async Task<int> GetBrojKlijenataAsync(int radnikId)
    {
        return await _context.Termini
            .Where(t => t.RadnikID == radnikId)
            .Select(t => t.KlijentID)
            .Distinct()
            .CountAsync();
    }
}
