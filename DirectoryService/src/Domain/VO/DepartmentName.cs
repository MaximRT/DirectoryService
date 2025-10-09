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

    public static Result<DepartmentName, Errors> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length < MIN_LENGTH_VALUE || trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        return Result.Success<DepartmentName, Errors>(new DepartmentName(trimmedValue));
    }
}