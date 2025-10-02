namespace Projeto.Domain.Entities;

public class Motorcycle : Traceable
{
    public required int Year { get; set; }
    public required string Model { get; set; }
    public required string RegistrationPlate { get; set; }
}
