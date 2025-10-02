namespace Projeto.Domain.Entities;

/// <summary>
/// Classe para representar locação de uma motocicleta por um entregador.
/// </summary>
public class Rental : Traceable
{
    private Rental() { }
    public Rental(Motorcycle motorcycle, Deliverer deliverer, DateTime startDate, DateTime estimatedEndDate, int plan)
    {
        if (deliverer.Licence.Type != Enums.LicenceType.A)
            throw new ArgumentException("Deliverer must have a valid A type licence to rent a motorcycle.");

        MotorcycleId = motorcycle.Id;
        DelivererId = deliverer.Id;
        StartDate = startDate;
        EstimatedEndDate = estimatedEndDate;
        Plan = plan;
    }

    public required string MotorcycleId { get; set; }
    public required string DelivererId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required DateTime EstimatedEndDate { get; set; }

    // TODO - Fazer entity para Plan, com número de dias, preço, multa por atraso, etc.
    public int Plan { get; set; }
    public DateTime? DeliveryDate { get; set; }
}
