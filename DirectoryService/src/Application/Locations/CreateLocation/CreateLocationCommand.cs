using Application.Abstractions;
using Contracts.Location;

namespace Application.Locations.CreateLocation;

public record CreateLocationCommand(string Name,
    LocationAddressDto Address,
    LocationTimezoneDto Timezone,
    bool IsActive) : ICommand;