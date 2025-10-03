using Projeto.Domain.Entities;

namespace Projeto.Tests.DomainTests;

public class MotorcycleTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        string id = "moto123";
        string model = "Moto Ducati";
        int year = 2025;
        string plateNumber = "PNR-2605";

        // Act
        Motorcycle motorcycle = new(id, model, year, plateNumber);

        // Assert
        Assert.Equal(id, motorcycle.Id);
        Assert.Equal(model, motorcycle.Model);
        Assert.Equal(year, motorcycle.Year);
        Assert.Equal(plateNumber, motorcycle.PlateNumber);
    }

    [Fact]
    public void Constructor_WithInvalidPlate_ShouldThrow()
    {
        // Arrange
        string id = "moto123";
        string model = "Moto Ducati";
        int year = 2025;
        string plateNumber = "123123";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Motorcycle(id, model, year, plateNumber));
    }

    [Fact]
    public void UpdateRegistrationPlate_ShouldUpdateCorrectly()
    {
        // Arrange
        string id = "moto123";
        string model = "Moto Ducati";
        int year = 2025;
        string plateNumber = "PNR-2605";

        string newPlateNumber = "BNL-2812";

        // Act
        Motorcycle motorcycle = new(id, model, year, plateNumber); 
        
        motorcycle.UpdateRegistrationPlate(newPlateNumber);

        // Assert
        Assert.Equal(newPlateNumber, motorcycle.PlateNumber);
    }

    [Fact]
    public void UpdateRegistrationPlate_WithInvalidPlate_ShouldThrow()
    {
        // Arrange
        string id = "moto123";
        string model = "Moto Ducati";
        int year = 2025;
        string plateNumber = "PNR-2605";

        string newPlateNumber = "123123";

        // Act
        Motorcycle motorcycle = new(id, model, year, plateNumber);

        // Assert
        Assert.Throws<ArgumentException>(() => motorcycle.UpdateRegistrationPlate(newPlateNumber));
    }
}
