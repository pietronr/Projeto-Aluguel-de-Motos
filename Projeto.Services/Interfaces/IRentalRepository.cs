using Projeto.Domain.Entities;
using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IRentalRepository
{
    Task<RentalResponse?> GetAsync(string id);
    void Insert(Rental rental);
    Task<bool> AnyAsync(string id);
}
