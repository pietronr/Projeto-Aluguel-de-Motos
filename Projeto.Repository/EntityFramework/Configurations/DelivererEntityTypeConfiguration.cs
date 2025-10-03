using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain.Entities;

namespace Projeto.Repository.EntityFramework.Configurations;

internal class DelivererEntityTypeConfiguration : IEntityTypeConfiguration<Deliverer>
{
    public void Configure(EntityTypeBuilder<Deliverer> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(d => d.Name)
               .IsRequired();

        builder.Property(d => d.RegistryCode)
               .IsRequired();

        builder.Property(d => d.BirthDate)
               .IsRequired();

        builder.Ignore(d => d.IsValidForRental);

        builder.OwnsOne(d => d.Licence, licence =>
        {
            licence.Property(l => l.Number)
                   .IsRequired();

            licence.Property(l => l.Type)
                   .IsRequired()
                   .HasConversion<string>();

            licence.Ignore(l => l.Image);
        });
    }
}
