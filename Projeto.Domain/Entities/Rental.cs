using Projeto.Domain.Builders;

namespace Projeto.Domain.Entities;

/// <summary>
/// Classe para representar locação de uma motocicleta por um entregador.
/// </summary>
public class Rental : Traceable
{
    private static readonly int[] PlansWithAdvanceFee = [7, 15];
    private static readonly Dictionary<int, PlanFees> Plans = new()
    {
        { 7, new(30.0m, 0.20m) },
        { 15, new(28.0m, 0.40m) },
        { 30, new(22.0m) },
        { 45, new(20.0m) },
        { 50, new(18.0m) },
    };

    private Rental() { }
    public Rental(Motorcycle motorcycle, Deliverer deliverer, DateTime startDate, DateTime estimatedEndDate, int dayPlan)
    {
        if (deliverer.Licence.Type != Enums.LicenceType.A)
            throw new ArgumentException("Deliverer must have a valid A type licence to rent a motorcycle.");
        if (!Plans.ContainsKey(dayPlan))
            throw new ArgumentException("Invalid rental plan.");

        MotorcycleId = motorcycle.Id;
        DelivererId = deliverer.Id;
        StartDate = startDate;
        EstimatedEndDate = estimatedEndDate;
        DayPlan = dayPlan;
    }

    public readonly decimal DelayFine = 50.0m;

    public string MotorcycleId { get; init; } = string.Empty;
    public string DelivererId { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime EstimatedEndDate { get; init; }
    public int DayPlan { get; init; }
    public DateTime? DeliveryDate { get; private set; }

    public bool HasAdvanceFee => PlansWithAdvanceFee.Contains(DayPlan);

    public bool IsClosed { get; private set; }

    public void CloseRental(DateTime deliveryDate)
    {
        DeliveryDate = deliveryDate;
        IsClosed = true;
    }

    public PlanFees GetPlanFees() => Plans[DayPlan];

    public decimal GetDailyFee() => GetPlanFees().DailyFee;
}

public record struct PlanFees(decimal DailyFee, decimal AdvanceFee = 0.0m);
