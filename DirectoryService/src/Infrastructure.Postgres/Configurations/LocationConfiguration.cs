using Domain.Entity;
using Domain.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.Property(l => l.LocationId).HasColumnName("location_id");

        builder.HasKey(l => l.LocationId).HasName("pk_locations");

        builder.Property(la => la.Name)
            .HasConversion(
                n => n.Name,
                n => LocationName.Create(n).Value)
            .HasColumnName("name")
            .IsRequired();

        builder.OwnsMany(l => l.Addresses, lb =>
        {
            lb.ToJson("addresses");

            lb.Property(la => la.Country)
                .HasColumnName("country")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

            lb.Property(la => la.Region)
                .HasColumnName("region")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

            lb.Property(la => la.City)
                .HasColumnName("city")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

            lb.Property(la => la.Street)
                .HasColumnName("street")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

            lb.Property(la => la.House)
                .HasColumnName("house")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

            lb.Property(la => la.Apartment)
                .HasColumnName("apartment")
                .HasMaxLength(ProjectsConsts.MaxLenght150)
                .IsRequired();

        });

        builder.Property(la => la.Timezone)
            .HasConversion(
                t => t.IanaCode,
                t => LocationTimezone.Create(t).Value)
            .HasColumnName("timezone")
            .IsRequired();

        builder.Property(l => l.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(l => l.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(l => l.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasMany(l => l.DepartmentLocations)
            .WithOne()
            .HasForeignKey(dl => dl.LocationId);

    }
}