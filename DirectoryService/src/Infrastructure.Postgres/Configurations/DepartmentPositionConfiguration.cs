using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Postgres.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.Property(dp => dp.DepartmentPositionId).HasColumnName("department_position_id");

        builder.HasKey(d => d.DepartmentPositionId).HasName("pk_department_positions");

        builder.Property(dp => dp.PositionId).HasColumnName("position_id");

        builder.HasOne(d => d.Department)
            .WithMany(d => d.DepartmentPositions)
            .HasForeignKey("department_id")
            .OnDelete(DeleteBehavior.Cascade);

        // тоже самое по аналогии
        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(dp => dp.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}