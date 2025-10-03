using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record DepartmentName
{
    private const int MIN_LENGTH_VALUE = 3;
    private const int MAX_LENGTH_VALUE = 150;

    public string Name { get; }

    private DepartmentName(string value)
    {
        Name = value;
    }

    public static Result<DepartmentName, Error> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue))
        {
            return Error.Validation(
                null,
                "DepartmentName.Value cannot be empty",
                Status.VALIDATION,
                null);
        }

        if (trimmedValue.Length < MIN_LENGTH_VALUE || trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return Error.Validation(
                null,
                $"DepartmentName.Value should be >= {MIN_LENGTH_VALUE} and <= {MAX_LENGTH_VALUE}",
                Status.VALIDATION,
                null);
        }

        return Result.Success<DepartmentName, Error>(new DepartmentName(trimmedValue));
    }
}