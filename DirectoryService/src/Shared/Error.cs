namespace Shared;

public class Error
{
    public static Error None = new Error(null, null, Shared.Status.NONE, null);

    public string? Code { get; set; }

    public string? Message { get; set; }

    public Status Status { get; set; }

    public string? InvalidField { get; set; }

    private Error(string? code, string? message, Status status, string? invalidField)
    {
        Code = code;
        Message = message;
        Status = status;
        InvalidField = invalidField;
    }

    public static Error Validation(string? code, string? message, Status status, string? invalidField) =>
        new (code ?? "value.is.invalid", message, status, invalidField);

    public static Error NotFound(string? code, string? message, Status status) =>
        new (code ?? "value.is.notfound", message, status, null);

    public static Error Failure(string? code, string? message, Status status) =>
        new (code ?? "failure.on.server", message, status, null);

    public static Error Conflict(string? code, string? message, Status status) =>
        new (code ?? "value.is.conflict", message, status, null);
}

public enum Status
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