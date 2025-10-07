using Application.Locations;
using CSharpFunctionalExtensions;
using Domain.Entity;
using Infrastructure.Postgres.Data;
using Shared;

namespace Infrastructure.Postgres.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid, Error>> CreateAsync(Location location, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return location.LocationId;
        }
        catch (Exception ex)
        {
            return Error.Failure(null, ex.Message, Status.FAILURE);
        }
    }
}