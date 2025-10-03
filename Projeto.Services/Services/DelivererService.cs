using Projeto.Domain.Entities;
using Projeto.Domain.Enums;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Services.Services;

/// <summary>
/// Classe serviço para os entregadores, repsonsável por operações CRUD e interações com a camada de repositório.
/// </summary>
/// <param name="repository">Camada de repositório.</param>
/// <param name="uow">Unit of work para persistências.</param>
public class DelivererService(IDelivererRepository repository, IUnitOfWork uow) : IDelivererService
{
    public async Task<Result> InsertAsync(DelivererRequest request)
    {
        try
        {
            Deliverer deliverer = new(request.Identificador, request.Nome, request.Cnpj, request.DataNascimento, 
                request.NumeroCnh, Enum.Parse<LicenceType>(request.TipoCnh), request.ImagemCnh);

            repository.Insert(deliverer);
            _ = await uow.SaveChangesAsync();

            return Result.Created();
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            return Result.Fail("Dados inválidos");
        }
    }

    public async Task<Result> UpdateLicenceImageAsync(string id, UpdateDelivererImageRequest licenceImage)
    {
        try
        {
            Deliverer? deliverer = await repository.GetAsync(id);

            // Preferiria retornar 404. Seguindo requisitos.
            if (deliverer is null)
                return Result.Fail("Dados inválidos");

            deliverer.Licence.UpdateImage(licenceImage.ImagemCnh);

            //TODO - FAZER LÓGICA PARA ATUALIZAÇÃO DE IMAGEM AQUI

            return Result.Created();
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            return Result.Fail("Dados inválidos");
        }
    }
}
