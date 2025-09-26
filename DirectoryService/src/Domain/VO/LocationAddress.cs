using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record LocationAddress
{
    private const int MAX_LENGTH = 200;

    public string Country { get; }
    public string Region { get; }
    public string City { get; }
    public string Street { get; }
    public string House { get; }
    public string? Apartment { get; }

    private LocationAddress(
        string country,
        string region,
        string city,
        string street,
        string house,
        string? apartment)
    {
        Country = country;
        Region = region;
        City = city;
        Street = street;
        House = house;
        Apartment = apartment;
    }

    public static Result<LocationAddress, Error> Create(
        string country,
        string region,
        string city,
        string street,
        string house,
        string? apartment)
    {
        var trimmedCountry = country?.Trim();
        var trimmedRegion = region?.Trim();
        var trimmedCity = city?.Trim();
        var trimmedStreet = street?.Trim();
        var trimmedHouse = house?.Trim();
        var trimmedApartment = apartment?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedCountry))
        {
            return Error.Validation(
                null,
                "Address.Country cannot be empty.",
                Status.VALIDATION,
                null);
        }

        if (string.IsNullOrWhiteSpace(trimmedCity))
        {
            return Error.Validation(
                null,
                "Address.City cannot be empty.",
                Status.VALIDATION,
                null);
        }

        if (string.IsNullOrWhiteSpace(trimmedStreet))
        {
            return Error.Validation(
                null,
                "Address.Street cannot be empty.",
                Status.VALIDATION,
                null);
        }

        if (string.IsNullOrWhiteSpace(trimmedHouse))
        {
            return Error.Validation(
                null,
                "Address.House cannot be empty.",
                Status.VALIDATION,
                null);
        }

        if (new[] { trimmedCountry, trimmedCity, trimmedStreet, trimmedHouse }
            .Any(x => x!.Length > MAX_LENGTH))
        {
            return Error.Validation(
                null,
                "Some required address fields are too long.",
                Status.VALIDATION,
                null);
        }

        return Result.Success<LocationAddress, Error>(
            new LocationAddress(
                trimmedCountry!,
                trimmedRegion ?? string.Empty,
                trimmedCity!,
                trimmedStreet!,
                trimmedHouse!,
                trimmedApartment));
    }
}