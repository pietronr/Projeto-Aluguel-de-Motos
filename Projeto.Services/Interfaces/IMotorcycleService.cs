using Projeto.Services.Dtos;

namespace Projeto.Services.Interfaces;

public interface IMotorcycleService
{
    Task<Result<IEnumerable<MotorcycleDto>>> GetAllAsync(string? plateNumber);
    Task<Result<MotorcycleDto>> GetAsync(string id);
    Task<Result> InsertAsync(MotorcycleDto motorcycleDto);
    Task<Result> UpdatePlateAsync(string id, UpdateMotorcyclePlateDto plateDto);
    Task<Result> DeleteAsync(string id);
}
