namespace Projeto.Domain.Entities;

public class Traceable
{
    public required string Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
