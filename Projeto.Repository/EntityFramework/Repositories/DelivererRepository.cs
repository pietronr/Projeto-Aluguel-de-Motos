using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

/// <summary>
/// Classe repositório que lida com a persistência e sobrescrita de dados relacionados aos entregadores.
/// </summary>
/// <param name="context">DbContext da aplicação.</param>
public class DelivererRepository(ProjetoContext context) : IDelivererRepository
{
    private readonly DbSet<Deliverer> _dbSet = context.Deliverers;

    public async Task<Deliverer?> GetAsync(string id)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Insert(Deliverer deliverer)
    {
        _dbSet.Add(deliverer);
    }
}
