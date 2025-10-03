using Projeto.Domain.Entities;
using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IMotorcycleRepository
{
    Task<IEnumerable<MotorcycleDto>> GetAllAsync(string? plateNumber);
    Task<MotorcycleDto?> GetAsync(string id);
    Task<Motorcycle?> GetTrackedAsync(string id);
    Task<bool> HasRentalAsync(string id);
    Task<bool> AnyAsync(string? id, string? plateNumber);
    void Insert(Motorcycle motorcycle);
    void Remove(Motorcycle motorcycle);
}
