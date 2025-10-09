using Application.Abstractions;
using Application.Locations.CreateLocation;
using Contracts.Location;
using Microsoft.AspNetCore.Mvc;
using Presentation.EndpointsResult;

namespace Presentation.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    [HttpPost]
    public async Task<EndpointResult<Guid>> Create(
        [FromServices] ICommandHandler<Guid, CreateLocationCommand> handler,
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateLocationCommand(request.Name, request.Address, request.Timezone, request.IsActive);
        return await handler.Handle(command, cancellationToken);
    }
}