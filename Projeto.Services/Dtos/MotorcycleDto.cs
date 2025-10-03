using Projeto.Domain.Entities;

namespace Projeto.Services.Dtos;

/// <summary>
/// Classe DTO para requests e responses, adequando ao modelo do Swagger.
/// </summary>
public class MotorcycleDto
{
    public required string Identificador { get; set; }
    public required int Ano { get; set; }
    public required string Modelo { get; set; } = string.Empty;
    public required string Placa { get; set; } = string.Empty;

    public static MotorcycleDto FromEntity(Motorcycle entity)
    {
        return new MotorcycleDto
        {
            Identificador = entity.Id,
            Ano = entity.Year,
            Modelo = entity.Model,
            Placa = entity.PlateNumber
        };
    }
}
