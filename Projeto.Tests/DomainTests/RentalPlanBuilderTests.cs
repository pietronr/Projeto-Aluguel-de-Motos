using Projeto.Domain.Builders;
using Projeto.Domain.Entities;
using Projeto.Domain.Enums;

namespace Projeto.Tests.DomainTests;

public class RentalPlanBuilderTests
{
    private readonly Motorcycle _motorcycle;
    private readonly Deliverer _validForRentalDeliverer;

    public RentalPlanBuilderTests()
    {
        _motorcycle = new("moto123", "Moto Ducati", 2025, "PNR-2605");

        _validForRentalDeliverer = new Deliverer("entregador123", "Pietro", "12.345.678/0001-95", new DateTime(1990, 1, 1),
            "71852574952", LicenceType.A, "image.png");
    }

    [Fact]
    public void Calculate_WithoutClosingRental_ShouldThrow()
    {
        // Arrange
        int dayPlan = 7;
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddDays(dayPlan);
        DateTime deliveryDate = endDate;

        Rental rental = new(_motorcycle.Id, _validForRentalDeliverer.Id, startDate, endDate, dayPlan);

        Assert.Throws<InvalidOperationException>(() => new RentalPlanBuilder(rental));
    }

    [Theory]
    [InlineData(7, 210.0)]
    [InlineData(15, 420.0)]
    [InlineData(30, 660.0)]
    [InlineData(45, 900.0)]
    [InlineData(50, 900.0)]
    public void Calculate_WithCorrectDeliveryDate_RentalPlanBuilder(int dayPlan, decimal expectedValue)
    {
        // Arrange
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddDays(dayPlan);
        DateTime deliveryDate = endDate;

        Rental rental = new(_motorcycle.Id, _validForRentalDeliverer.Id, startDate, endDate, dayPlan);
        rental.CloseRental(deliveryDate);

        RentalPlanBuilder builder = new(rental);

        decimal totalValue = builder
            .CalculateAdvanceFine()
            .CalculateDelayFine()
            .CalculateRentalTotalFee()
            .GetRentalTotalFee();

        Assert.Equal(expectedValue, totalValue);
    }

    [Theory]
    [InlineData(7, 310.0)]
    [InlineData(15, 520.0)]
    [InlineData(30, 760.0)]
    [InlineData(45, 1000.0)]
    [InlineData(50, 1000.0)]
    public void Calculate_WithDelayedDeliveryDate_RentalPlanBuilder(int dayPlan, decimal expectedValue)
    {
        // Arrange
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddDays(dayPlan);
        DateTime deliveryDate = endDate.AddDays(2);

        Rental rental = new(_motorcycle.Id, _validForRentalDeliverer.Id, startDate, endDate, dayPlan);
        rental.CloseRental(deliveryDate);

        RentalPlanBuilder builder = new(rental);

        decimal totalValue = builder
            .CalculateAdvanceFine()
            .CalculateDelayFine()
            .CalculateRentalTotalFee()
            .GetRentalTotalFee();

        Assert.Equal(expectedValue, totalValue);
    }

    [Theory]
    [InlineData(7, 222.0)]
    [InlineData(15, 442.4)]
    [InlineData(30, 660.0)]
    [InlineData(45, 900.0)]
    [InlineData(50, 900.0)]
    public void Calculate_WithAdvanceDeliveryDate_RentalPlanBuilder(int dayPlan, decimal expectedValue)
    {
        // Arrange
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddDays(dayPlan);
        DateTime deliveryDate = endDate.AddDays(-2);

        Rental rental = new(_motorcycle.Id, _validForRentalDeliverer.Id, startDate, endDate, dayPlan);
        rental.CloseRental(deliveryDate);

        RentalPlanBuilder builder = new(rental);

        decimal totalValue = builder
            .CalculateAdvanceFine()
            .CalculateDelayFine()
            .CalculateRentalTotalFee()
            .GetRentalTotalFee();

        Assert.Equal(expectedValue, totalValue);
    }
}
