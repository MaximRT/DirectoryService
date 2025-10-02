using Domain.Entity;
using Domain.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.Property(d => d.Id).HasColumnName("department_id");

        builder.HasKey(d => d.Id).HasName("pk_departments");

        builder.Property(d => d.Name)
            .HasConversion(
                n => n.Name,
                n => DepartmentName.Create(n).Value)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(d => d.Identifier)
            .HasConversion(
                i => i.Identifier,
                i => DepartmentIdentifier.Create(i).Value)
            .HasColumnName("identifier")
            .IsRequired();

        builder.Property(d => d.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id");

        builder.Property(d => d.Path)
            .HasConversion(
                p => p.Path,
                p => DepartmentPath.Create(p).Value)
            .HasColumnName("path")
            .IsRequired();

        builder.Property(d => d.Depth)
            .IsRequired()
            .HasColumnName("depth");

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(d => d.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .IsRequired(false)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(d => d.DepartmentLocations)
            .WithOne(d => d.Department)
            .HasForeignKey(dl => dl.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.DepartmentPositions)
            .WithOne(dp => dp.Department)
            .HasForeignKey(dp => dp.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}