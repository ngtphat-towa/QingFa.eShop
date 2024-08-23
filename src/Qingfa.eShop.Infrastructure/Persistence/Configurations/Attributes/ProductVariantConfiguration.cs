using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes
{
    internal class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {

            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.ProductId)
                .IsRequired();

            builder.Property(pv => pv.SKU)
                .IsRequired()
                .HasMaxLength(100);

            builder.ComplexProperty(p => p.Price, price =>
            {
                price.Property(p => p.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            });

            builder.Property(pv => pv.StockLevel)
                .IsRequired();

            builder.Property(pv => pv.VariantStatus)
                .IsRequired();

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
