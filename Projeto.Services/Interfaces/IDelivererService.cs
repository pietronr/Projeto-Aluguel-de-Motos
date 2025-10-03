using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IDelivererService
{
    Task<Result> InsertAsync(DelivererRequest request);
    Task<Result> SendLicenceImageAsync(string id, string licenceImage);
}
