namespace Projeto.Domain.Entities;

public class Rental
{
    public required string MotorcycleId { get; set; }
    // TODO - Apenas entregadores com carteira A podem alugar motos
    public required string DelivererId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required DateTime EstimatedEndDate { get; set; }
    public int Plan { get; set; }
}
