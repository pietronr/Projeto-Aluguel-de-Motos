using Projeto.Domain.Entities;
using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IMotorcycleRepository
{
    Task<IEnumerable<MotorcycleDto>> GetAllAsync(string? plateNumber = null);
    Task<MotorcycleDto?> GetAsync(string id);
    void Insert(Motorcycle motorcycle);
    void Remove(Motorcycle motorcycle);
    Task<bool> AnyAsync(string id);
}
