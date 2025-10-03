using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

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
