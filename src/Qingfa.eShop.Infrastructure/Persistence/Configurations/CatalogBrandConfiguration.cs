using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CatalogBrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // Define the table name
            builder.ToTable("CatalogBrands");

            // Define the primary key
            builder.HasKey(cb => cb.Id);

            // Define the properties and their constraints
            builder.Property(cb => cb.Id)
                .HasConversion(
                    id => id.Value,
                    value => CatalogBrandId.Create(value))
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.Bio)
                .HasMaxLength(500);

            builder.Property(cb => cb.LogoURL)
                .HasMaxLength(500);

            builder.Property(cb => cb.WebsiteURL)
                .HasMaxLength(500);

            builder.Property(cb => cb.CountryOfOrigin)
                .HasMaxLength(100);

            builder.Property(cb => cb.EstablishmentYear)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
