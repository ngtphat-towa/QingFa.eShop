using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes
{
    internal class ProductAttributeGroupConfiguration : IEntityTypeConfiguration<ProductAttributeGroup>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeGroup> builder)
        {
            builder.HasKey(vag => vag.Id);

            builder.Property(vag => vag.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(vag => vag.Attributes)
                .WithOne(pa => pa.AttributeGroup)
                .HasForeignKey(pa => pa.AttributeGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}