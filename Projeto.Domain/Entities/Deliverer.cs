using Projeto.Domain.Enums;

namespace Projeto.Domain.Entities;

public class Deliverer : Traceable
{
    public required string Name { get; set; }
    // TODO - Fazer ValueObject para CNPJ
    public required string CorporateCode { get; set; }
    public required DateTime BirthDate { get; set; }
    // TODO - Fazer classe para CNH
    public required string RegistrationNumber { get; set; }
    public RegistrationType RegistrationType { get; set; }
    public required string RegistrationImage { get; set; }
}
