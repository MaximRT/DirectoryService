using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record DepartmentPath
{
    private const int MAX_LENGTH_VALUE = 200;
    public string Path { get; }

    private DepartmentPath(string value)
    {
        Path = value;
    }

    public static Result<DepartmentPath, Errors> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        return Result.Success<DepartmentPath, Errors>(new DepartmentPath(trimmedValue));
    }
};