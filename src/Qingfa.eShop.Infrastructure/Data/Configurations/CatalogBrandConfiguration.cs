using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            // Configure primary key
            builder.HasKey(e => e.Id);

            // Configure properties with concise constraints
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false); 

            builder.Property(e => e.LogoURL)
                .HasMaxLength(200)
                .IsRequired(false); 

            builder.Property(e => e.WebsiteURL)
                .HasMaxLength(200)
                .IsRequired(false); 

            builder.Property(e => e.CountryOfOrigin)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.EstablishmentYear)
                .IsRequired();

            // Configure the value object (CatalogBrandId) behavior
            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    value => CatalogBrandId.Create(value));
        }
    }
}
