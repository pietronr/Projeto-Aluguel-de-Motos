using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto.Domain.ValueObjects;

namespace Projeto.Repository.Helpers;

public class RegistrationPlateConverter : ValueConverter<RegistrationPlate, string>
{
    public RegistrationPlateConverter() : base(v => v.Number, v => new RegistrationPlate(v)) { }

}

public class RegistryConverter : ValueConverter<Registry, string>
{
    public RegistryConverter() : base(v => v.Code, v => new Registry(v)) { }

}
