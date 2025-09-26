using CSharpFunctionalExtensions;
using Shared;

namespace Domain.Entity;

public class Position
{
    private const int MIN_LENGTH_NAME = 3;
    private const int MAX_LENGTH_NAME = 100;
    private const int MAX_LENGTH_DESCRIPTION = 1000;

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private Position() { }

    private Position(string name, string? description, bool isActive)
    {
        Id = Guid.NewGuid();
        Name = name!;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Result<Position, Error> Create(string name, string? description, bool isActive)
    {
        var trimmedName = name.Trim();
        var trimmedDescription = description?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedName))
        {
            return Error.Validation(
                null,
                "Position name cannot be null or whitespace.",
                Status.VALIDATION,
                null);
        }

        if (trimmedName.Length < MIN_LENGTH_NAME || trimmedName.Length > MAX_LENGTH_NAME)
        {
            return Error.Validation(
                    null,
                    $"Position name should be >= {MIN_LENGTH_NAME} and <= {MAX_LENGTH_NAME}",
                    Status.VALIDATION,
                    null);
        }

        if (trimmedDescription?.Length > MAX_LENGTH_DESCRIPTION)
        {
            return Error.Validation(
                null,
                $"Position description should be <= {MAX_LENGTH_DESCRIPTION}",
                Status.VALIDATION,
                null);
        }

        return Result.Success<Position, Error>(new Position(trimmedName, trimmedDescription, isActive));
    }
}