using Projeto.Domain.Entities;

namespace Projeto.Tests.DomainTests;

public class RentalTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var motorcycleId = "moto123";
        var delivererId = "entregador123";
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(7);
        var dayPlan = 7;

        // Act
        var rental = new Rental(motorcycleId, delivererId, startDate, endDate, dayPlan);

        // Assert
        Assert.Equal(motorcycleId, rental.MotorcycleId);
        Assert.Equal(delivererId, rental.DelivererId);
        Assert.Equal(startDate, rental.StartDate);
        Assert.Equal(endDate, rental.EndDate);
        Assert.Equal(endDate, rental.EstimatedEndDate);
        Assert.Equal(dayPlan, rental.DayPlan);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(-7)]
    public void Constructor_InvalidPlan_ShouldThrow(int invalidPlan)
    {
        // Arrange
        var motorcycleId = "moto123";
        var delivererId = "entregador123";
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(7);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Rental(motorcycleId, delivererId, startDate, endDate, invalidPlan));
    }

    [Theory]
    [InlineData(7, true)]
    [InlineData(15, true)]
    [InlineData(30, false)]
    [InlineData(45, false)]
    [InlineData(50, false)]
    public void HasAdvanceFee_ReturnsExpected(int plan, bool expected)
    {
        // Arrange
        var rental = new Rental("moto123", "entregador123", DateTime.Today, DateTime.Today.AddDays(plan), plan);

        // Act
        var result = rental.HasAdvanceFee;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CloseRental_ShouldSetDeliveryDateAndIsClosed()
    {
        // Arrange
        var rental = new Rental("moto123", "entregador123", DateTime.Today, DateTime.Today.AddDays(7), 7);
        var deliveryDate = DateTime.Today.AddDays(7);

        // Act
        rental.CloseRental(deliveryDate);

        // Assert
        Assert.True(rental.IsClosed);
        Assert.Equal(deliveryDate, rental.DeliveryDate);
    }

    [Theory]
    [InlineData(7, 30.0)]
    [InlineData(15, 28.0)]
    [InlineData(30, 22.0)]
    [InlineData(45, 20.0)]
    [InlineData(50, 18.0)]
    public void GetPlanFees_ShouldReturnCorrectDailyFee(int plan, decimal expectedFee)
    {
        // Arrange
        var rental = new Rental("moto123", "entregador123", DateTime.Today, DateTime.Today.AddDays(plan), plan);

        // Act
        var fees = rental.GetPlanFees();

        // Assert
        Assert.Equal(expectedFee, fees.DailyFee);
    }

    [Theory]
    [InlineData(7, 30.0)]
    [InlineData(15, 28.0)]
    [InlineData(30, 22.0)]
    [InlineData(45, 20.0)]
    [InlineData(50, 18.0)]
    public void GetDailyFee_ShouldReturnCorrectFee(int plan, decimal expectedFee)
    {
        // Arrange
        var rental = new Rental("moto123", "entregador123", DateTime.Today, DateTime.Today.AddDays(plan), plan);

        // Act
        var fee = rental.GetDailyFee();

        // Assert
        Assert.Equal(expectedFee, fee);
    }
}
