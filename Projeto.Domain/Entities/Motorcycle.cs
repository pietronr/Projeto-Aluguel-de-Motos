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

    public int Year { get; internal set; }
    public string Model { get; internal set; } = string.Empty;
    public RegistrationPlate PlateNumber { get; internal set; }

    public void UpdateRegistrationPlate(string newPlateNumber)
    {
        PlateNumber = new(newPlateNumber);
    }
}
