using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain.Entities;

namespace Projeto.Repository.EntityFramework.Configurations;

internal class MotorcycleEntityTypeConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(m => m.Year)
               .IsRequired();

        builder.Property(m => m.Model)
               .IsRequired();

        builder.Property(m => m.PlateNumber)
               .IsRequired();
    }
}
