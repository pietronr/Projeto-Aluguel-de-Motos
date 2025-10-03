using Projeto.Domain.Entities;
using Projeto.Domain.Helpers;

namespace Projeto.Domain.Builders;

/// <summary>
/// Classe builder para calcular a taxa total de uma locação com base no plano escolhido, datas de início e entrega.
/// </summary>
public class RentalPlanBuilder(Rental rental)
{
    private readonly bool _initialized = true;
    private readonly PlanFees _currentPlan = rental.GetPlanFees();

    private decimal _advanceFine;
    private decimal _delayFine;
    private decimal _rentalTotalFee;

    public RentalPlanBuilder CalculateAdvanceFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (rental.DeliveryDate < rental.EstimatedEndDate && rental.HasAdvanceFee)
        {
            var daysInAdvance = (rental.EstimatedEndDate - rental.DeliveryDate.TryValue()).Days;
            _advanceFine = daysInAdvance * _currentPlan.DailyFee *  _currentPlan.AdvanceFee;
        }

        return this;
    }

    public RentalPlanBuilder CalculateDelayFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (rental.DeliveryDate > rental.EstimatedEndDate)
        {
            var delayedDays = (rental.DeliveryDate.TryValue() - rental.EstimatedEndDate).Days;
            _delayFine = delayedDays * rental.DelayFine;
        }

        return this;
    }

    public RentalPlanBuilder CalculateRentalTotalFee()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        var rentalDays = (rental.DeliveryDate.TryValue() - rental.StartDate).Days;
        var baseRentalFee = rentalDays * _currentPlan.DailyFee;
        _rentalTotalFee = baseRentalFee + _advanceFine + _delayFine;

        return this;
    }

    public decimal GetRentalTotalFee() => _rentalTotalFee;
}