using Projeto.Domain.Entities;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.Services.Services;

/// <summary>
/// Classe serviço para as motos, repsonsável por operações CRUD e interações com a camada de repositório.
/// </summary>
/// <param name="repository">Camada de repositório.</param>
/// <param name="uow">Unit of work para persistências.</param>
public class MotorcycleService(IMotorcycleRepository repository, IUnitOfWork uow) : IMotorcycleService
{
    public async Task<Result<IEnumerable<MotorcycleDto>>> GetAllAsync(string? plateNumber)
    {
        IEnumerable<MotorcycleDto> motorcycles = await repository.GetAllAsync(plateNumber);

        return Result<IEnumerable<MotorcycleDto>>.Success(motorcycles);
    }

    public async Task<Result<MotorcycleDto>> GetAsync(string id)
    {
        MotorcycleDto? motorcycle = await repository.GetAsync(id);

        if (motorcycle == null)
            return Result<MotorcycleDto>.Fail("Moto não encontrada");

        return Result<MotorcycleDto>.Success(motorcycle);
    }

    public async Task<Result> InsertAsync(MotorcycleDto motorcycleDto)
    {
        try
        {
            Motorcycle motorcycle = new(motorcycleDto.Identificador, motorcycleDto.Modelo, motorcycleDto.Ano, motorcycleDto.Placa);

            repository.Insert(motorcycle);
            _ = await uow.SaveChangesAsync();

            return Result.Created();
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            return Result.Fail("Dados inválidos");
        }
    }

    public async Task<Result> UpdateAsync(string id, UpdateMotorcyclePlateDto plateDto)
    {
        try
        {
            Motorcycle? motorcycle = await repository.GetTrackedAsync(id);

            if (motorcycle == null)
                return Result.Fail("Dados inválidos");

            motorcycle.UpdateRegistrationPlate(plateDto.Placa);

            _ = await uow.SaveChangesAsync();

            return Result.Ok("Placa modificada com sucesso");
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            return Result.Fail("Dados inválidos");
        }
    }

    public async Task<Result> DeleteAsync(string id)
    {
        Motorcycle? motorcycle = await repository.GetTrackedAsync(id);

        if (motorcycle == null)
            return Result.Fail("Dados inválidos");

        repository.Remove(motorcycle);

        _ = await uow.SaveChangesAsync();

        return Result.Ok();
    }
}
