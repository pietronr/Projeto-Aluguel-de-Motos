using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IRentalService
{
    Task<Result<RentalResponse>> GetAsync(string id);
    Task<Result> InsertAsync(RentalRequest request);
    Task<Result<RentalResultResponse>> CloseRentalAsync(string id, UpdateRentalDeliveryDateRequest deliveryDateRequest);
}
