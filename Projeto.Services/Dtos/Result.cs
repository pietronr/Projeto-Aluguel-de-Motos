using System.Text.Json.Serialization;

namespace Projeto.Services.Dtos;

/// <summary>
/// Classe a ser usada para retornos do serviço.
/// </summary>
public class Result
{
    private Result(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    [JsonIgnore]
    public bool IsSuccess { get; }

    [JsonPropertyName("mensagem")]
    public string? Message { get; }

    public static Result Created() => new(true, null);
    public static Result Ok() => new(true, null);
    public static Result Ok(string message) => new(true, message);
    public static Result Fail(string message) => new(false, message);
}

/// <summary>
/// Classe genérica a ser usada para retornos no serviço quando há um objeto a ser retornado.
/// </summary>
/// <typeparam name="T">Tipo do objeto a ser retornado.</typeparam>
public class Result<T> where T : class
{
    private Result(T? response, bool isSuccess, string? message)
    {
        Response = response;
        IsSuccess = isSuccess;
        Message = message;
    }

    [JsonIgnore]
    public T? Response { get; }

    [JsonIgnore]
    public bool IsSuccess { get; }

    [JsonPropertyName("mensagem")]
    public string? Message { get; }

    public static Result<T> Success(T response) => new(response, true, null);
    public static Result<T> Fail(string message) => new(null, false, message);
}
