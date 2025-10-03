using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

/// <summary>
/// Classe repositório que lida com a persistência e sobrescrita de dados relacionados às motos.
/// </summary>
/// <param name="context">DbContext da aplicação.</param>
public class MotorcycleRepository(ProjetoContext context) : IMotorcycleRepository
{
    private readonly DbSet<Motorcycle> _dbSet = context.Motorcycles;
    private readonly DbSet<Rental> _rental = context.Rentals;

    public async Task<IEnumerable<MotorcycleDto>> GetAllAsync(string? plateNumber)
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

    public async Task<Motorcycle?> GetTrackedAsync(string id) => await _dbSet.FindAsync(id);

    public Task<bool> HasRentalAsync(string id) => _rental.AnyAsync(x => x.MotorcycleId == id);

    public Task<bool> IdExistsAsync(string id) => _dbSet.AnyAsync(x => x.Id == id);

    public Task<bool> PlateNumberExistsAsync(string plateNumber) => _dbSet.AnyAsync(x => x.PlateNumber == plateNumber);

    public void Insert(Motorcycle motorcycle)
    {
        _dbSet.Add(motorcycle);
    }

    public void Remove(Motorcycle motorcycle)
    {
        _dbSet.Remove(motorcycle);
    }
}
