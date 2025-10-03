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
    public Rental(string motorcycleId, string delivererId, DateTime startDate, DateTime endDate, int dayPlan)
    {
        if (!Plans.ContainsKey(dayPlan))
            throw new ArgumentException("Invalid rental plan.");

        MotorcycleId = motorcycleId;
        DelivererId = delivererId;
        StartDate = startDate;
        EndDate = endDate;
        EstimatedEndDate = endDate;
        DayPlan = dayPlan;
    }

    public readonly decimal DelayFine = 50.0m;

    public string MotorcycleId { get; private set; } = string.Empty;
    public string DelivererId { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime EstimatedEndDate { get; private set; }
    public int DayPlan { get; private set; }
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
