using Projeto.Domain.Entities;
using System.Text.Json.Serialization;

namespace Projeto.Services.Dtos;

/// <summary>
/// Classe DTO para requests de locação, adequando ao modelo do Swagger.
/// </summary>
public class RentalRequest
{
    [JsonPropertyName("entregador_id")]
    public required string EntregadorId { get; set; }

    [JsonPropertyName("moto_id")]
    public required string MotoId { get; set; }

    [JsonPropertyName("data_inicio")]
    public required DateTime DataInicio { get; set; }

    [JsonPropertyName("data_termino")]
    public required DateTime DataTermino { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public required DateTime DataPrevisaoTermino { get; set; }

    public required int Plano { get; set; }
}

/// <summary>
/// Classe DTO para responses de locação, adequando ao modelo do Swagger.
/// </summary>
public class RentalResponse
{
    public required string Identificador { get; set; }

    [JsonPropertyName("valor_diaria")]
    public required decimal ValorDiaria { get; set; }

    [JsonPropertyName("entregador_id")]
    public required string EntregadorId { get; set; }

    [JsonPropertyName("moto_id")]
    public required string MotoId { get; set; }

    [JsonPropertyName("data_inicio")]
    public required DateTime DataInicio { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime? DataTermino { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public required DateTime DataPrevisaoTermino { get; set; }

    [JsonPropertyName("data_devolucao")]
    public DateTime? DataDevolucao { get; set; }

    public static RentalResponse FromEntity(Rental entity)
    {
        return new RentalResponse
        {
            Identificador = entity.Id,
            ValorDiaria = entity.GetDailyFee(),
            EntregadorId = entity.DelivererId,
            MotoId = entity.MotorcycleId,
            DataInicio = entity.StartDate,
            DataTermino = entity.EndDate,
            DataPrevisaoTermino = entity.EstimatedEndDate,
            DataDevolucao = entity.DeliveryDate
        };
    }
}

/// <summary>
/// Classe DTO para atualização da data de devolução da locação.
/// </summary>
public class UpdateRentalDeliveryDateRequest
{
    [JsonPropertyName("data_devolucao")]
    public required DateTime DataDevolucao { get; set; }
}
