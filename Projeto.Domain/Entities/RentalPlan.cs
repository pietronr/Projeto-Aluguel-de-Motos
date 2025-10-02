namespace Projeto.Domain.Entities;

public class RentalPlanBuilder
{
    private readonly bool _initialized = false;
    private static readonly decimal DelayFine = 50.0m;
    private static readonly int[] PlansWithAdvanceFee = [7, 15];
    private static readonly Dictionary<int, PlanFees> Plans = new()
    {
        { 7, new(30.0m, 0.20m) },
        { 15, new(28.0m, 0.40m) },
        { 30, new(22.0m) },
        { 45, new(20.0m) },
        { 50, new(18.0m) },
    };

    private PlanConfiguration _configuration;
    private PlanFees _currentPlan;
    private decimal _advanceFine;
    private decimal _delayFine;
    private decimal _rentalTotalFee;

    public RentalPlanBuilder(int dayPlan, DateTime startDate, DateTime deliveryDate)
    {
        if (!IsValidPlan(dayPlan))
            throw new ArgumentException("Invalid rental plan.");

        var estimatedEndDate = startDate.AddDays(dayPlan);

        _configuration = new(dayPlan, startDate, estimatedEndDate, deliveryDate, PlansWithAdvanceFee.Contains(dayPlan));
        _currentPlan = Plans[dayPlan];
        _initialized = true;
    }

    public RentalPlanBuilder CalculateAdvanceFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (_configuration.DeliveryDate < _configuration.EstimatedEndDate && _configuration.ShouldConsiderAdvanceFee)
        {
            var daysInAdvance = (_configuration.EstimatedEndDate - _configuration.DeliveryDate).Days;
            _advanceFine = daysInAdvance * _currentPlan.DailyFee *  _currentPlan.AdvanceFee;
        }

        return this;
    }

    public RentalPlanBuilder CalculateDelayFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (_configuration.DeliveryDate > _configuration.EstimatedEndDate)
        {
            var delayedDays = (_configuration.DeliveryDate - _configuration.EstimatedEndDate).Days;
            _delayFine = delayedDays * DelayFine;
        }

        return this;
    }

    public RentalPlanBuilder CalculateRentalTotalFee()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        var rentalDays = (_configuration.DeliveryDate - _configuration.StartDate).Days;
        var baseRentalFee = rentalDays * _currentPlan.DailyFee;
        _rentalTotalFee = baseRentalFee + _advanceFine + _delayFine;

        return this;
    }

    public decimal GetRentalTotalFee() => _rentalTotalFee;

    public static bool IsValidPlan(int dayPlan) => Plans.ContainsKey(dayPlan);
}

public record struct PlanConfiguration(int DayPlan, DateTime StartDate, DateTime EstimatedEndDate, DateTime DeliveryDate, bool ShouldConsiderAdvanceFee);
public record struct PlanFees(decimal DailyFee, decimal AdvanceFee = 0.0m);
