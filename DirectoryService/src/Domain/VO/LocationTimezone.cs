using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record LocationTimezone
{
    public string IanaCode { get; }

    private LocationTimezone(string ianaCode)
    {
        IanaCode = ianaCode;
    }

    public static Result<LocationTimezone, Errors> Create(string ianaCode)
    {
        var trimmedValue = ianaCode?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue))
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(trimmedValue);
            return Result.Success<LocationTimezone, Errors>(
                new LocationTimezone(trimmedValue));
        }
        catch (TimeZoneNotFoundException)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }
        catch (InvalidTimeZoneException)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }
    }
}