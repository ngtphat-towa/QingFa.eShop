using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes
{
    internal class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.ProductVariantId)
                .IsRequired();

            builder.Property(v => v.ProductAttributeId)
                .IsRequired();

            builder.Property(v => v.ProductAttributeOptionId)
                .IsRequired();

            builder.Property(v => v.Value)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(v => v.IsRequired)
                .IsRequired();

            builder.Property(v => v.IsVisibleToCustomer)
                .IsRequired();

            builder.HasOne(v => v.Variant)
                .WithMany(pv => pv.VariantAttributes)
                .HasForeignKey(v => v.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Attribute)
                .WithMany(pa => pa.VariantAttributes)
                .HasForeignKey(v => v.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Option)
                .WithMany()
                .HasForeignKey(v => v.ProductAttributeOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(v => new { v.ProductVariantId, v.ProductAttributeId, v.ProductAttributeOptionId })
                .IsUnique();
        }
    }

}
