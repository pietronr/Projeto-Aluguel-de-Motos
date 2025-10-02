namespace Projeto.Domain.ValueObjects;

/// <summary>
/// Tipo valor para representar placas de veículos. Faz a validação do formato do número placa ao inicializar.
/// </summary>
public readonly struct RegistrationPlate
{
    public RegistrationPlate(string number)
    {
        if (!IsValidRegistrationPlate(number))
            throw new ArgumentException("Invalid registry code.");

        Number = number;
    }

    public string Number { get; init; }

    public static implicit operator string(RegistrationPlate registry) => registry.Number;

    public static bool IsValidRegistrationPlate(string plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
            return false;

        plate = plate.Replace("-", "").ToUpper();

        var oldPattern = @"^[A-Z]{3}[0-9]{4}$";
        var mercosulPattern = @"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";

        return System.Text.RegularExpressions.Regex.IsMatch(plate, oldPattern)
            || System.Text.RegularExpressions.Regex.IsMatch(plate, mercosulPattern);
    }
}
