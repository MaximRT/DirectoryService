using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record DepartmentIdentifier
{
    private const int MIN_LENGTH_VALUE = 3;
    private const int MAX_LENGTH_VALUE = 150;
    private static readonly Regex _pattern = new("^[a-zA-Z]+$", RegexOptions.Compiled);

    public string Value { get; }

    private DepartmentIdentifier(string value)
    {
        Value = value;
    }

    public static Result<DepartmentIdentifier, Error> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue))
        {
            return Error.Validation(
                null,
                "DepartmentIdentifier.Value cannot be empty",
                Status.VALIDATION,
                null);
        }

        if (trimmedValue.Length < MIN_LENGTH_VALUE || trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return Error.Validation(
                null,
                $"DepartmentIdentifier.Value should be >= {MIN_LENGTH_VALUE} and <= {MAX_LENGTH_VALUE}",
                Status.VALIDATION,
                null);
        }

        if (!_pattern.IsMatch(trimmedValue))
        {
            return Error.Validation(
                null,
                "DepartmentIdentifier.Value must consist only latin letters",
                Status.VALIDATION,
                null);
        }

        return Result.Success<DepartmentIdentifier, Error>(new DepartmentIdentifier(trimmedValue));
    }
};