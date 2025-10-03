using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain.Entities;

namespace Projeto.Repository.EntityFramework.Configurations;

internal class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(r => r.StartDate)
            .IsRequired();

        builder.Property(r => r.EndDate)
            .IsRequired();

        builder.Property(r => r.EstimatedEndDate)
            .IsRequired();

        builder.Property(r => r.Plan)
            .IsRequired();

        builder.HasOne<Motorcycle>()
               .WithMany()
               .HasForeignKey(r => r.MotorcycleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Deliverer>()
               .WithMany()
               .HasForeignKey(r => r.DelivererId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
