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
            // Define the table name
            builder.ToTable("CatalogBrands");

            // Define the primary key
            builder.HasKey(cb => cb.Id);

            // Define the properties and their constraints
            builder.Property(cb => cb.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CatalogBrandId(value))
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.Description)
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
