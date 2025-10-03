using Projeto.Domain.ValueObjects;

namespace Projeto.Domain.Entities;

/// <summary>
/// Classe para representar motocicletas.
/// </summary>
public class Motorcycle : Traceable
{
    private Motorcycle() { }

    public Motorcycle(string id, string model, int year, string plateNumber)
    {
        Id = id;
        Model = model;
        Year = year;
        PlateNumber = new(plateNumber);
    }

    public int Year { get; private set; }
    public string Model { get; private set; } = string.Empty;
    public RegistrationPlate PlateNumber { get; private set; }

    public void UpdateRegistrationPlate(string newPlateNumber)
    {
        PlateNumber = new(newPlateNumber);
    }
}
