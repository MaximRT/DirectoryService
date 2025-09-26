using CSharpFunctionalExtensions;
using Domain.VO;
using Shared;

namespace Domain.Entity;

public class Department
{
    private const int MIN_VALUE_DEPTH = 1;
    private List<Department> _children = [];
    private List<DepartmentLocation> _departmentLocations = [];
    private List<DepartmentPosition> _departmentPositions = [];

    public Guid Id { get; private set; }

    public DepartmentName Name { get; private set; }

    public DepartmentIdentifier Identifier { get; private set; }

    public Guid? ParentId { get; private set; }

    public DepartmentPath Path { get; private set; }

    public int Depth { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<Department> Children => _children;

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;

    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    private Department() { }

    private Department(
        DepartmentName name,
        DepartmentIdentifier identifier,
        DepartmentPath path,
        int depth,
        bool isActive,
        IEnumerable<Guid> departmentLocations)
    {
        Id = Guid.NewGuid();
        Name = name;
        Identifier = identifier;
        Path = path;
        Depth = depth;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        _departmentLocations = departmentLocations
            .Select(departmentLocation => DepartmentLocation.Create(this, departmentLocation).Value)
            .ToList();
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }

    public UnitResult<Error> Rename(string newName)
    {
        var result = DepartmentName.Create(newName);

        if (result.IsFailure)
            return UnitResult.Failure(result.Error);

        Name = result.Value;

        return UnitResult.Success<Error>();
    }

    public static Result<Department, Error> Create(
        DepartmentName name,
        DepartmentIdentifier identifier,
        DepartmentPath path,
        int depth,
        bool isActive,
        IEnumerable<Guid> departmentLocations)
    {
        if (depth < MIN_VALUE_DEPTH)
        {
            return Error.Validation(
                    null,
                    $"Department.Depth cannot be less than {MIN_VALUE_DEPTH}.",
                    Status.VALIDATION,
                    null);
        }

        return Result.Success<Department, Error>(new Department(name, identifier, path, depth, isActive, departmentLocations));
    }
}