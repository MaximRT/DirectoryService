using CSharpFunctionalExtensions;
using Shared;

namespace Domain.Entity;

public class DepartmentPosition
{
    public Guid DepartmentPositionId { get; private set; }

    public Department Department { get; private set; }

    public Guid PositionId { get; private set; }

    private DepartmentPosition() { }

    private DepartmentPosition(Department department, Guid positionId)
    {
        DepartmentPositionId = Guid.NewGuid();
        Department = department;
        PositionId = positionId;
    }

    public static Result<DepartmentPosition, Error> Create(Department department, Guid positionId)
    {
        return Result.Success<DepartmentPosition, Error>(new DepartmentPosition(department, positionId));
    }
}