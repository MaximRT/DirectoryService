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

    public static Result<LocationTimezone, Error> Create(string ianaCode)
    {
        var trimmedValue = ianaCode?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedValue))
        {
            return Error.Validation(
                null,
                "LocationTimezone.Value cannot be empty.",
                Status.VALIDATION,
                null);
        }

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(trimmedValue);
            return Result.Success<LocationTimezone, Error>(
                new LocationTimezone(trimmedValue));
        }
        catch (TimeZoneNotFoundException)
        {
            return Error.Validation(
                null,
                $"LocationTimezone.IANACode is invalid value: '{trimmedValue}'.",
                Status.VALIDATION,
                null);
        }
        catch (InvalidTimeZoneException)
        {
            return Error.Validation(
                null,
                $"LocationTimezone.IANACode is invalid timezone format: '{trimmedValue}'.",
                Status.VALIDATION,
                null);
        }
    }
}