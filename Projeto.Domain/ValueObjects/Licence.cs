using Projeto.Domain.Enums;

namespace Projeto.Domain.ValueObjects;

public struct Licence
{
    public Licence(string number, LicenceType type, string image)
    {
        if (!IsValidLicence(number))
            throw new ArgumentException("Invalid registry code.");

        Number = number;
        Type = type;
        Image = image;
    }

    public readonly LicenceType Type { get; init; }
    public string Number { get; init; }

    //TODO - IMAGEM DEVE SER GRAVADA EM UM SERVIÇO DE ARMAZENAMENTO, NÃO NO BANCO.
    // TALVEZ FAZER CLASSE PARA LIDAR COM IMAGENS?
    public string Image { get; private set; } = string.Empty;

    public static implicit operator string(Licence licence) => licence.Number;

    public static bool IsValidLicence(string cnh)
    {
        if (string.IsNullOrWhiteSpace(cnh))
            return false;

        cnh = new string([.. cnh.Where(char.IsDigit)]);

        if (cnh.Length != 11)
            return false;

        if (cnh.Distinct().Count() == 1)
            return false;

        int soma = 0;
        int dv1, dv2;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(cnh[i].ToString()) * (9 - i);

        dv1 = soma % 11;
        dv1 = dv1 >= 10 ? 0 : dv1;

        soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cnh[i].ToString()) * (i + 1);

        dv2 = soma % 11;
        dv2 = dv2 >= 10 ? 0 : dv2;

        return cnh[9].ToString() == dv1.ToString() && cnh[10].ToString() == dv2.ToString();
    }

    //TODO - IMPLEMENTAR ISSO
    public void UpdateImage(string newImage)
    {
        Image = newImage;
    }
}
