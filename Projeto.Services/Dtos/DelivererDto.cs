using System.Text.Json.Serialization;

namespace Projeto.Services.Dtos;

/// <summary>
/// Classe DTO para requests de entregadores, adequando ao modelo do Swagger.
/// </summary>
public class DelivererRequest
{
    public required string Identificador { get; set; }
    public required string Nome { get; set; }
    public required string Cnpj { get; set; }

    [JsonPropertyName("data_nascimento")]
    public required DateTime DataNascimento { get; set; }

    [JsonPropertyName("numero_cnh")]    
    public required string NumeroCnh { get; set; }

    [JsonPropertyName("tipo_cnh")]
    public required string TipoCnh { get; set; }

    [JsonPropertyName("imagem_cnh")]
    public required string ImagemCnh { get; set; }
}

/// <summary>
/// Classe DTO para atualização da imagem da CNH do entregador.
/// </summary>
public class UpdateDelivererImageRequest
{
    [JsonPropertyName("imagem_cnh")]
    public required string ImagemCnh { get; set; }
}
