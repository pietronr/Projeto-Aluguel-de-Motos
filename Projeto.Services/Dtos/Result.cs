using System.Text.Json.Serialization;

namespace Projeto.Services.Dtos;

public class Result
{
    private Result(bool isSuccess, string? mensagem)
    {
        IsSuccess = isSuccess;
        Message = mensagem;
    }

    public bool IsSuccess { get; init; }

    [JsonPropertyName("mensagem")]
    public string? Message { get; init; }

    public static Result Success() => new(true, null);
    public static Result Fail(string message) => new(false, message);
}
