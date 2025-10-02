using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.Property(d => d.DepartmentLocationId).HasColumnName("department_location_id");

        builder.HasKey(d => d.DepartmentLocationId).HasName("pk_department_locations");

        builder.Property(d => d.LocationId).HasColumnName("location_id");

        builder.HasOne(dl => dl.Department)
            .WithMany(d => d.DepartmentLocations)
            .HasForeignKey("department_id")
            .OnDelete(DeleteBehavior.Cascade);

        // По идеи такую связь нельзя делать если Location это другой модуль / bounded context
        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(dl => dl.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}