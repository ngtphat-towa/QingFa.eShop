using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Attributes
{
    public class ProductAttributeOptionConfiguration : IEntityTypeConfiguration<ProductAttributeOption>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeOption> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.OptionValue)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.Property(a => a.IsDefault)
                .IsRequired();

            builder.Property(a => a.SortOrder)
                .IsRequired();

            builder.Property(a => a.ProductAttributeId)
                .IsRequired();

            builder.HasOne(a => a.Attribute)
                .WithMany(pa => pa.AttributeOptions)
                .HasForeignKey(a => a.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
