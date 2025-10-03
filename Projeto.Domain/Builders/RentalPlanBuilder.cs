using Projeto.Domain.Entities;
using Projeto.Domain.Helpers;

namespace Projeto.Domain.Builders;

/// <summary>
/// Classe builder para calcular a taxa total de uma locação com base no plano escolhido, datas de início e entrega.
/// </summary>
public class RentalPlanBuilder
{
    private readonly bool _initialized;
    private readonly PlanFees _currentPlan;
    private readonly Rental _rental;

    public RentalPlanBuilder(Rental rental)
    {
        if (!rental.IsClosed)
            throw new InvalidOperationException("Rental must be closed to calculate and initialize builder");

        _rental = rental;
        _currentPlan = rental.GetPlanFees();
        _initialized = true;

    }

    private decimal _advanceFine;
    private decimal _delayFine;
    private decimal _rentalTotalFee;

    public RentalPlanBuilder CalculateAdvanceFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (_rental.DeliveryDate < _rental.EstimatedEndDate && _rental.HasAdvanceFee)
        {
            var daysInAdvance = (_rental.EstimatedEndDate - _rental.DeliveryDate.TryValue()).Days;
            _advanceFine = daysInAdvance * _currentPlan.DailyFee *  _currentPlan.AdvanceFee;
        }

        return this;
    }

    public RentalPlanBuilder CalculateDelayFine()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        if (_rental.DeliveryDate > _rental.EstimatedEndDate)
        {
            var delayedDays = (_rental.DeliveryDate.TryValue() - _rental.EstimatedEndDate).Days;
            _delayFine = delayedDays * _rental.DelayFine;
        }

        return this;
    }

    public RentalPlanBuilder CalculateRentalTotalFee()
    {
        if (!_initialized)
            throw new InvalidOperationException("Configuration must be initialized before calculating rental fee.");

        var rentalDays = (_rental.EstimatedEndDate - _rental.StartDate).Days;
        var baseRentalFee = rentalDays * _currentPlan.DailyFee;
        _rentalTotalFee = baseRentalFee + _advanceFine + _delayFine;

        return this;
    }

    public decimal GetRentalTotalFee() => _rentalTotalFee;
}