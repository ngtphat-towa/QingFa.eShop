using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Aggregates;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Common.Enums;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("CatalogItems");

            builder.HasKey(c => c.Id);

            builder.Property(ca => ca.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.ShortDescription)
                .HasMaxLength(500);

            builder.Property(c => c.Description)
                .HasMaxLength(2000);

            builder.Property(c => c.CategoryId)
                .IsRequired()
                .HasConversion(id => id.Value, value => new CategoryId(value));

            builder.Property(c => c.BrandId)
                .IsRequired()
                .HasConversion(id => id.Value, value => new BrandId(value));

            // Configure the Price value object
            builder.ComplexProperty(c => c.Price, priceBuilder =>
            {
                priceBuilder.Property(p => p.Amount)
                    .IsRequired();

                priceBuilder.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(3); 
            });

            builder.Property(c => c.SKU)
                .HasMaxLength(50);

            builder.Property(c => c.StockLevel)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion(status => status.ToString(), statusStr => Enum.Parse<EntityStatus>(statusStr));

            // Configure indexes
            builder.HasIndex(c => c.SKU)
                .HasDatabaseName("IX_CatalogItems_SKU");


            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
