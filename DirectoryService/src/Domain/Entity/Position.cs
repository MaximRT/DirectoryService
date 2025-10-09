using CSharpFunctionalExtensions;
using Shared;

namespace Domain.Entity;

public class Position
{
    private const int MIN_LENGTH_NAME = 3;
    private const int MAX_LENGTH_NAME = 100;
    private const int MAX_LENGTH_DESCRIPTION = 1000;

    private List<DepartmentPosition> _departmentPositions = [];

    public Guid PositionId { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    private Position() { }

    private Position(string name, string? description, bool isActive)
    {
        PositionId = Guid.NewGuid();
        Name = name!;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Result<Position, Errors> Create(string name, string? description, bool isActive)
    {
        var trimmedName = name.Trim();
        var trimmedDescription = description?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedName)
            || trimmedName.Length < MIN_LENGTH_NAME
            || trimmedName.Length > MAX_LENGTH_NAME
            || trimmedDescription?.Length > MAX_LENGTH_DESCRIPTION)
        {
            return GeneralErrors.ValueIsInvalid().ToErrors();
        }

        return Result.Success<Position, Errors>(new Position(trimmedName, trimmedDescription, isActive));
    }
}