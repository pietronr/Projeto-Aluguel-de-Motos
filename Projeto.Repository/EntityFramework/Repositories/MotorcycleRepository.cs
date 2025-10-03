using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

/// <summary>
/// Classe de repositório para a entidade Motorcycle, implementando a interface IMotorcycleRepository.
/// </summary>
/// <param name="context"></param>
public class MotorcycleRepository(ProjetoContext context) : IMotorcycleRepository
{
    private readonly DbSet<Motorcycle> _dbSet = context.Motorcycles;

    public Task<bool> AnyAsync(string id) => _dbSet.AnyAsync(m => m.Id == id);  

    public async Task<IEnumerable<MotorcycleDto>> GetAllAsync(string? plateNumber = null)
    {
        return await _dbSet.Where(x => plateNumber == null || x.PlateNumber == plateNumber)
                    .Select(x => MotorcycleDto.FromEntity(x))
                    .AsNoTracking()
                    .ToListAsync();
    }

    public async Task<MotorcycleDto?> GetAsync(string id)
    {
        return await _dbSet.Where(x => x.Id == id)
                    .Select(x => MotorcycleDto.FromEntity(x))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
    }

    public void Insert(Motorcycle motorcycle)
    {
        _dbSet.Add(motorcycle);
    }

    public void Remove(Motorcycle motorcycle)
    {
        _dbSet.Remove(motorcycle);
    }
}
