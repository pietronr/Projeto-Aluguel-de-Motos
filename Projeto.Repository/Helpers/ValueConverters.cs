using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto.Domain.ValueObjects;

namespace Projeto.Repository.Helpers;

public class RegistrationPlateConverter : ValueConverter<RegistrationPlate, string>
{
    public RegistrationPlateConverter() : base(v => v.Number, v => new RegistrationPlate(v)) { }
}
