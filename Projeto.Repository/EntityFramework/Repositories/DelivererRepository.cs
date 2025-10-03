using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework.Repositories;

public class DelivererRepository(ProjetoContext context) : IDelivererRepository
{
    private readonly DbSet<Deliverer> _dbSet = context.Deliverers;

    public Task<bool> AnyAsync(string id) => _dbSet.AnyAsync(m => m.Id == id);

    public void Insert(Deliverer deliverer)
    {
        _dbSet.Add(deliverer);
    }
}
