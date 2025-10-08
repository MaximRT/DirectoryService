namespace Shared;

public class Error
{
    public static Error None = new Error(null, null, Shared.ErrorType.NONE, null);

    public string? Code { get; set; }

    public string? Message { get; set; }

    public ErrorType Type { get; set; }

    public string? InvalidField { get; set; }

    private Error(string? code, string? message, ErrorType status, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = status;
        InvalidField = invalidField;
    }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new (code, message, ErrorType.VALIDATION, invalidField);

    public static Error NotFound(string code, string message) => new (code, message, ErrorType.NOT_FOUND);

    public static Error Failure(string code, string message) => new (code, message,  ErrorType.FAILURE);

    public static Error Conflict(string code, string message) => new (code, message,  ErrorType.CONFLICT);

    public Errors ToErrors() => new([this]);
}

public enum ErrorType
{
    /// <summary>
    /// Ошибка отсутствует
    /// </summary>
    NONE,

    /// <summary>
    /// Ошибка валидации
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Ошибка отсутствия ресурса
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Неизвестная ошибка
    /// </summary>
    FAILURE,

    /// <summary>
    /// Ошибка при выполнеии действия
    /// </summary>
    CONFLICT,
}