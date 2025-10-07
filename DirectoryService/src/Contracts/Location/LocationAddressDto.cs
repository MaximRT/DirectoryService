namespace Contracts.Location;

public record LocationAddressDto(
    string Country,
    string Region,
    string City,
    string Street,
    string House,
    string? Apartment);
