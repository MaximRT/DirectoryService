using CSharpFunctionalExtensions;
using Domain.VO;
using Shared;

namespace Domain.Entity;

public class Location
{
    private List<DepartmentLocation> _departmentLocations = [];

    private List<LocationAddress> _locationAddresses = [];

    public Guid LocationId { get; private set; }

    public LocationName Name { get; private set; }

    public IReadOnlyList<LocationAddress> Addresses => _locationAddresses;

    public LocationTimezone Timezone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;

    private Location() {}

    private Location(LocationName name, LocationAddress address, LocationTimezone timezone, bool isActive)
    {
        LocationId = Guid.NewGuid();
        Name = name;
        _locationAddresses.Add(address);
        Timezone = timezone;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Result<Location, Error> Create(LocationName name, LocationAddress address, LocationTimezone timezone,
        bool isActive)
    {
        return Result.Success<Location, Error>(new Location(name, address, timezone, isActive));
    }
}