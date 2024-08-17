using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingFa.EShop.Domain.Catalogs.Variants;
using QingFa.EShop.Domain.Catalogs.Products;
using QingFa.EShop.Domain.Catalogs.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
        {
            builder.ToTable("ProductVariantAttributes");

            builder.HasKey(pva => pva.Id);

            builder.Property(pva => pva.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => new ProductVariantAttributeId(value)
                );

            builder.Property(pva => pva.ProductVariantId)
                .HasColumnName("ProductVariantId")
                .HasConversion(
                    id => id.Value,
                    value => new ProductVariantId(value)
                );

            builder.Property(pva => pva.AttributeId)
                .HasColumnName("AttributeId")
                .HasConversion(
                    id => id.Value,
                    value => new ProductAttributeId(value)
                );

            builder.Property(pva => pva.AttributeOptionId)
                .HasColumnName("AttributeOptionId")
                .HasConversion(
                    id => id == null ? default : id.Value,
                    value => new AttributeOptionId(value)
                );

            builder.Property(pva => pva.CustomValue)
                .HasColumnName("CustomValue")
                .HasMaxLength(255); // Adjust length as needed

            builder.Property(pva => pva.IsRequired)
                .HasColumnName("IsRequired");

            builder.Property(pva => pva.IsVisibleToCustomer)
                .HasColumnName("IsVisibleToCustomer");

            // Add a timestamp for concurrency control (optional)
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            // Configure relationships
            builder.HasOne(pva => pva.ProductVariant)
                .WithMany(pv => pv.ProductVariantAttributes)
                .HasForeignKey(pva => pva.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pva => pva.Attribute)
                .WithMany()
                .HasForeignKey(pva => pva.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pva => pva.AttributeOption)
                .WithMany()
                .HasForeignKey(pva => pva.AttributeOptionId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(pva => new { pva.ProductVariantId, pva.AttributeId })
                .HasDatabaseName("IX_ProductVariantAttributes_ProductVariantId_AttributeId");
        }
    }
}
