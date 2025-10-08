namespace Shared;

public record Envelope
{
    public object? Result { get; }

    public Errors? ErrorList { get; }

    public bool IsError => ErrorList != null || (ErrorList != null && ErrorList.Any());

    public DateTime TimeGenerated { get; }

    public Envelope(object? result, Errors? errorList)
    {
        Result = result;
        ErrorList = errorList;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object? result = null) => new Envelope(result, null);

    public static Envelope Error(Errors errors) => new Envelope(null, errors);
};

public record Envelope<T>
{
    public T? Result { get; }

    public Errors? ErrorList { get; }

    public bool IsError => ErrorList != null || (ErrorList != null && ErrorList.Any());

    public DateTime TimeGenerated { get; }

    public Envelope(T? result, Errors? errorList)
    {
        Result = result;
        ErrorList = errorList;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(T? result = default) => new Envelope(result, null);

    public static Envelope Error(Errors errors) => new Envelope(null, errors);
};