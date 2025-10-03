using Projeto.Domain.Entities;

namespace Projeto.Services.Interfaces;

public interface IDelivererRepository
{
    void Insert(Deliverer deliverer);
    Task<bool> AnyAsync(string id);
}
