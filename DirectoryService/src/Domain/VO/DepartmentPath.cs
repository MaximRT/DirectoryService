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

    public static Result<DepartmentPath, Error> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue))
        {
            return Error.Validation(
                null,
                "DepartmentPath.Value cannot be empty.",
                Status.VALIDATION,
                null);
        }

        if (trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return Error.Validation(
                null,
                "DepartmentPath.Value is too long.",
                Status.VALIDATION,
                null);
        }

        return Result.Success<DepartmentPath, Error>(new DepartmentPath(trimmedValue));
    }
};