using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingFa.EShop.Domain.Catalogs.Variants;
using QingFa.EShop.Domain.Catalogs.Products;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            #region Table Configuration

            builder.ToTable("ProductVariants");

            #endregion

            #region Primary Key

            builder.HasKey(pv => pv.Id);

            #endregion

            #region Properties

            // Configure the Id property as a GUID column
            builder.Property(pv => pv.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => new ProductVariantId(value)
                );

            // Configure the ProductId property
            builder.Property(pv => pv.ProductId)
              .HasConversion(
                  id => id.Value,
                  value => new ProductId(value)
              );


            // Configure the SKU property
            builder.Property(pv => pv.SKU)
                .HasColumnName("SKU")
                .HasMaxLength(100) // Adjust length as needed
                .IsRequired();

            // Configure the Price property
            builder.Property(pv => pv.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)") 
                .IsRequired();

            // Configure the StockQuantity property
            builder.Property(pv => pv.StockQuantity)
                .HasColumnName("StockQuantity")
                .IsRequired();

            // Configure the IsActive property
            builder.Property(pv => pv.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            // Add a timestamp for concurrency control (optional)
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Relationships

            // Configure the relationship to Product
            builder.HasOne<Product>()
                .WithMany() 
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Indexes

            // Add an index on the SKU property
            builder.HasIndex(pv => pv.SKU)
                .IsUnique()
                .HasDatabaseName("IX_ProductVariants_SKU");

            #endregion
        }
    }
}
