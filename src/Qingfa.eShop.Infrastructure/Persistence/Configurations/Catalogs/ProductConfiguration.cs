using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.Catalogs.Categories;
using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Table Configuration

            builder.ToTable("Products");

            #endregion

            #region Primary Key

            builder.HasKey(p => p.Id);

            #endregion

            #region Properties

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value)
                );

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.CategoryId)
                .HasColumnName("CategoryId")
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value)
                );

            builder.Property(p => p.BrandId)
                .HasColumnName("BrandId")
                .HasConversion(
                    id => id.Value,
                    value => new BrandId(value)
                );

            builder.Property(p => p.StockQuantity)
                .HasColumnName("StockQuantity")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            // Add a timestamp for concurrency control (optional)
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Relationships

            // Configure the relationship to Category
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship to Brand
            builder.HasOne<Brand>()
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Indexes

            builder.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");

            #endregion
        }
    }
}
