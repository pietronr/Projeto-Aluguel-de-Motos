using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

/// <summary>
/// Classe repositório que lida com a persistência e sobrescrita de dados relacionados às locações.
/// </summary>
/// <param name="context">DbContext da aplicação.</param>
public class RentalRepository(ProjetoContext context) : IRentalRepository
{
    private readonly DbSet<Rental> _dbSet = context.Rentals;

    public async Task<RentalResponse?> GetAsync(string id)
    {
        return await _dbSet.Where(x => x.Id == id)
                    .Select(x => RentalResponse.FromEntity(x))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
    }

    public async Task<Rental?> GetTrackedAsync(string id) => await _dbSet.FindAsync(id);

    public void Insert(Rental rental)
    {
        _dbSet.Add(rental);
    }
}
