using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Configure the primary key
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(400);

            builder.Property(p => p.LongDescription)
                .HasMaxLength(1000);

            builder.ComplexProperty(p => p.Price, price =>
            {
                price.Property(p => p.Amount)
                   .HasColumnName("PriceAmount")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            });

            // Configure the many-to-many relationship with CategoryProduct
            builder.HasMany(p => p.CategoryProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship with Brand
            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            // Add index on Name for faster lookups
            builder.HasIndex(b => b.Name);
            builder.HasIndex(b => b.Status);
            builder.HasIndex(a => a.Created);
            builder.HasIndex(a => a.CreatedBy);
            builder.HasIndex(a => a.LastModified);
            builder.HasIndex(a => a.LastModifiedBy);
        }
    }
}
