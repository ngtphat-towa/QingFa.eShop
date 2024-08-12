using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CatalogCategoryConfiguration : IEntityTypeConfiguration<CatalogCategory>
    {
        public void Configure(EntityTypeBuilder<CatalogCategory> builder)
        {
            builder.ToTable("CatalogCategories");

            // Primary key
            builder.HasKey(cc => cc.Id);

            // Property configuration for CatalogCategoryId
            builder.Property(cc => cc.Id)
                .HasConversion(
                    id => id.Value.ToString(),
                    value => CatalogCategoryId.Create(int.Parse(value))
                )
                .HasColumnName("CatalogCategoryId")
                .IsRequired();

            // Properties
            builder.Property(cc => cc.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(cc => cc.Description)
                .HasMaxLength(1000);

            // Relationships
            builder.HasOne(cc => cc.ParentCategory)
                .WithMany()
                .HasForeignKey(cc => cc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
