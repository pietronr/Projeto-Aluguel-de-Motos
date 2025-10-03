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

    public string MotorcycleId { get; init; } = string.Empty;
    public string DelivererId { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime EstimatedEndDate { get; init; }
    public int Plan { get; init; }
    public DateTime? DeliveryDate { get; private set; }

    public void CloseRental(DateTime deliveryDate)
    {
        DeliveryDate = deliveryDate;
    }
}
