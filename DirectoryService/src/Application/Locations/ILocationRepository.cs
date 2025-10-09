using CSharpFunctionalExtensions;
using Domain.Entity;
using Shared;

namespace Application.Locations;

public interface ILocationRepository
{
    Task<Result<Guid, Errors>> CreateAsync(Location location, CancellationToken cancellationToken = default);
}