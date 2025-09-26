using CSharpFunctionalExtensions;
using Shared;

namespace Domain.Entity;

public class DepartmentLocation
{
    public Guid Id { get; private set; }

    public Department Department { get; private set; }

    public Guid LocationId { get; private set; }

    private DepartmentLocation() { }

    private DepartmentLocation(Department department, Guid locationId)
    {
        Id = Guid.NewGuid();
        Department = department;
        LocationId = locationId;
    }

    public static Result<DepartmentLocation, Error> Create(Department department, Guid locationId)
    {
        return Result.Success<DepartmentLocation, Error>(new DepartmentLocation(department, locationId));
    }
}