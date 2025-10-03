namespace Projeto.Domain.Entities;

/// <summary>
/// Classe base para entidades CRUD, contendo propriedades comuns de rastreamento.
/// </summary>
public class Traceable
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
