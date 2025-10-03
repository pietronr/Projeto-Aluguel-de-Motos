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

    public required string MotorcycleId { get; init; }
    public required string DelivererId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required DateTime EstimatedEndDate { get; init; }
    public int Plan { get; init; }
    public DateTime? DeliveryDate { get; private set; }

    public void CloseRental(DateTime deliveryDate)
    {
        DeliveryDate = deliveryDate;
    }
}
