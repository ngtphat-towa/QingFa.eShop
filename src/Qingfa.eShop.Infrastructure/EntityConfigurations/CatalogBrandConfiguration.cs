using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrands");

            // Primary key
            builder.HasKey(cb => cb.Id);

            // Property configuration for CatalogBrandId
            builder.Property(cb => cb.Id)
                .HasConversion(
                    id => id.Value.ToString(),
                    value => CatalogBrandId.Create(int.Parse(value))
                )
                .HasColumnName("CatalogBrandId")
                .IsRequired();

            // Properties
            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(cb => cb.Description)
                .HasMaxLength(1000);

            builder.Property(cb => cb.LogoURL)
                .HasMaxLength(500);

            builder.Property(cb => cb.WebsiteURL)
                .HasMaxLength(500);

            builder.Property(cb => cb.CountryOfOrigin)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.EstablishmentYear)
                .IsRequired();

        }
    }
}
