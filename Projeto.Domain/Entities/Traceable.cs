namespace Projeto.Domain.Entities;

/// <summary>
/// Classe base para entidades CRUD, contendo propriedades comuns de rastreamento.
/// </summary>
public class Traceable
{
    public required string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
