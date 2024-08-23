using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes
{
    internal class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pa => pa.AttributeCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pa => pa.Type)
                .IsRequired();

            builder.Property(pa => pa.IsRequired)
                .IsRequired();

            builder.Property(pa => pa.IsFilterable)
                .IsRequired();

            builder.Property(pa => pa.ShowToCustomers)
                .IsRequired();

            builder.Property(pa => pa.SortOrder)
                .IsRequired();

            builder.Property(pa => pa.AttributeGroupId)
                .IsRequired();

            builder.HasOne(pa => pa.AttributeGroup)
                .WithMany(vag => vag.Attributes)
                .HasForeignKey(pa => pa.AttributeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pa => pa.AttributeOptions)
                .WithOne(a => a.Attribute)
                .HasForeignKey(a => a.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pa => pa.AttributeCode).IsUnique();
        }
    }
}
