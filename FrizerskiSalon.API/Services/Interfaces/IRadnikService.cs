using FrizerskiSalon.API.DTOs;
using FrizerskiSalon.API.Entities;

namespace FrizerskiSalon.API.Services.Interfaces;

public interface IRadnikService
{
    Task<IEnumerable<Radnik>> GetAllAsync();
    Task<Radnik?> GetByIdAsync(int id);
    Task<Radnik> CreateAsync(Radnik radnik);
    Task<Radnik?> UpdateAsync(int id, Radnik updatedRadnik);
    Task<bool> DeleteAsync(int id);
    Task<int> GetBrojKlijenataAsync(int radnikId);
}
