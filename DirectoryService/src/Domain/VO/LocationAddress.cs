using CSharpFunctionalExtensions;
using Shared;

namespace Domain.VO;

public record LocationAddress
{
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

    public static Result<LocationAddress, Errors> Create(
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

        if (string.IsNullOrWhiteSpace(trimmedCountry)
            || string.IsNullOrWhiteSpace(trimmedCity)
            || string.IsNullOrWhiteSpace(trimmedStreet)
            || string.IsNullOrWhiteSpace(trimmedHouse)
            || new[] { trimmedCountry, trimmedCity, trimmedStreet, trimmedHouse }
                .Any(x => x!.Length > ProjectsConsts.MaxLenght200))
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        return Result.Success<LocationAddress, Errors>(
            new LocationAddress(
                trimmedCountry!,
                trimmedRegion ?? string.Empty,
                trimmedCity!,
                trimmedStreet!,
                trimmedHouse!,
                trimmedApartment));
    }
}