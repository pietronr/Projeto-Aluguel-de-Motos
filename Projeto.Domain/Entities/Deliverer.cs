using Projeto.Domain.Enums;
using Projeto.Domain.ValueObjects;

namespace Projeto.Domain.Entities;

/// <summary>
/// Classe para representar entregadores.
/// </summary>
public class Deliverer : Traceable
{
    private Deliverer() { }
    
    public Deliverer(string id, string name, string registryCode, DateTime birthDate, string licenceNumber, LicenceType licenceType, string licenceImage)
    {
        Id = id;
        Name = name;
        RegistryCode = new(registryCode);
        BirthDate = birthDate;
        Licence = new(licenceNumber, licenceType, licenceImage);
    }

    public string Name { get; init; } = string.Empty;
    public Registry RegistryCode { get; init; }
    public DateTime BirthDate { get; init; }
    public Licence Licence { get; init; }
}
