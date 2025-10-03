using Projeto.Domain.Entities;

namespace Projeto.Services.Interfaces;

public interface IDelivererRepository
{
    Task<Deliverer?> GetAsync(string id);
    Task<bool> RegistryExists(string registryCode);
    void Insert(Deliverer deliverer);
}
