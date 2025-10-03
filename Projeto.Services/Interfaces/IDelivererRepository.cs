using Projeto.Domain.Entities;

namespace Projeto.Services.Interfaces;

public interface IDelivererRepository
{
    Task<Deliverer?> GetAsync(string id);
    void Insert(Deliverer deliverer);
}
