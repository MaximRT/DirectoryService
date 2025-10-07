namespace Contracts.Location;

public record CreateLocationRequest(
    string Name,
    LocationAddressDto Address,
    LocationTimezoneDto Timezone,
    bool IsActive
    );
