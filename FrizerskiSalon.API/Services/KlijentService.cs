using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrizerskiSalon.API.Services
{
    public class KlijentService
    {
        private readonly FrizerskiSalonContext _context;

        public KlijentService(FrizerskiSalonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Klijent>> GetAllAsync()
        {
            return await _context.Klijenti.ToListAsync();
        }

        public async Task<Klijent?> GetByIdAsync(int id)
        {
            return await _context.Klijenti.FindAsync(id);
        }

        public async Task<Klijent> CreateAsync(Klijent klijent)
        {
            _context.Klijenti.Add(klijent);
            await _context.SaveChangesAsync();
            return klijent;
        }

        public async Task<bool> UpdateAsync(Klijent klijent)
        {
            var existing = await _context.Klijenti.FindAsync(klijent.Id);
            if (existing == null) return false;

            existing.Ime = klijent.Ime;
            existing.Prezime = klijent.Prezime;
            existing.BrTel = klijent.BrTel;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var klijent = await _context.Klijenti.FindAsync(id);
            if (klijent == null) return false;

            _context.Klijenti.Remove(klijent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUkupnoTerminaAsync(int klijentId)
        {
            return await _context.Termini
                .Where(t => t.KlijentID == klijentId)
                .CountAsync();
        }
    }
}
