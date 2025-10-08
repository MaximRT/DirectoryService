using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record DepartmentIdentifier
{
    private const int MIN_LENGTH_VALUE = 3;
    private const int MAX_LENGTH_VALUE = 150;
    private static readonly Regex _pattern = new("^[a-zA-Z]+$", RegexOptions.Compiled);

    public string Identifier { get; }

    private DepartmentIdentifier(string value)
    {
        Identifier = value;
    }

    public static Result<DepartmentIdentifier, Errors> Create(string value)
    {
        var trimmedValue = value?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length < MIN_LENGTH_VALUE || trimmedValue.Length > MAX_LENGTH_VALUE)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        if (!_pattern.IsMatch(trimmedValue))
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        return Result.Success<DepartmentIdentifier, Errors>(new DepartmentIdentifier(trimmedValue));
    }
};