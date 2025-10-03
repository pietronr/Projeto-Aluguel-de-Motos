namespace Projeto.Domain.Helpers;

public static class Extensions
{
    /// <summary>
    /// GetValueOrDefault com nome menor.
    /// </summary>
    /// <typeparam name="T">Tipo valor.</typeparam>
    /// <param name="value">Valor em si.</param>
    /// <returns></returns>
    public static T TryValue<T>(this T? value) where T : struct
    {
        return value.GetValueOrDefault();
    }
}
