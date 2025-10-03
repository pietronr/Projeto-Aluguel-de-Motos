using Projeto.Domain.Builders;
using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Services.Services;

/// <summary>
/// Classe serviço para os entregadores, repsonsável por operações CRUD e interações com a camada de repositório.
/// </summary>
/// <param name="repository">Camada de repositório.</param>
/// <param name="uow">Unit of work para persistências.</param>
public class RentalService(IRentalRepository repository, IDelivererRepository delivererRepository, 
    IMotorcycleRepository motorcycleRepository, IUnitOfWork uow) : IRentalService
{
    public async Task<Result<RentalResponse>> GetAsync(string id)
    {
        RentalResponse? rental = await repository.GetAsync(id);

        if (rental == null)
            return Result<RentalResponse>.Fail("Locação não encontrada");

        return Result<RentalResponse>.Success(rental);
    }

    public async Task<Result> InsertAsync(RentalRequest request)
    {
        try
        {
            Deliverer? deliverer = await delivererRepository.GetAsync(request.EntregadorId);

            if (deliverer == null || deliverer.IsValidForRental || !await motorcycleRepository.AnyAsync(request.MotoId, null))
                return Result.Fail("Dados inválidos");

            Rental rental = new(request.MotoId, deliverer.Id, request.DataInicio, request.DataTermino, request.Plano);

            repository.Insert(rental);
            _ = await uow.SaveChangesAsync();

            return Result.Created();
        }
        catch (Exception)
        {
            return Result.Fail("Dados inválidos");
        }
    }

    public async Task<Result<RentalResultResponse>> CloseRentalAsync(string id, UpdateRentalDeliveryDateRequest deliveryDateRequest)
    {
        Rental? rental = await repository.GetTrackedAsync(id);

        if (rental == null || rental.IsClosed)
            return Result<RentalResultResponse>.Fail("Dados inválidos");

        rental.CloseRental(deliveryDateRequest.DataDevolucao);
        _ = uow.SaveChangesAsync();

        RentalPlanBuilder builder = new(rental);

        decimal rentalTotalValue = builder
            .CalculateAdvanceFine()
            .CalculateDelayFine()
            .CalculateRentalTotalFee()
            .GetRentalTotalFee();

        // No swagger, apenas a mensagem é retornada, mas nos requisitos é dito que o valor deve retornar.
        // Dessa forma, alterei um pouco o retorno, incluindo o valor do cálculo. 
        RentalResultResponse result = new() { Mensagem = "Data de devolução informada com sucesso", ValorTotal = rentalTotalValue };

        return Result<RentalResultResponse>.Success(result);
    }
}
