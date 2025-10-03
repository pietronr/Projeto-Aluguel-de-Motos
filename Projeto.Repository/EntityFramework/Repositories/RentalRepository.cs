using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

public class RentalRepository(ProjetoContext context) : IRentalRepository
{
    private readonly DbSet<Rental> _dbSet = context.Rentals;

    public Task<bool> AnyAsync(string id) => _dbSet.AnyAsync(m => m.Id == id);

    public async Task<RentalResponse?> GetAsync(string id)
    {
        return await _dbSet.Where(x => x.Id == id)
                    .Select(x => RentalResponse.FromEntity(x))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
    }

    public void Insert(Rental rental)
    {
        _dbSet.Add(rental);
    }
}
