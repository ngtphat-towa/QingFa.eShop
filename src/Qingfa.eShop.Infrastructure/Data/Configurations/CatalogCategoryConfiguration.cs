using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class CatalogCategoryConfiguration : IEntityTypeConfiguration<CatalogCategory>
    {
        public void Configure(EntityTypeBuilder<CatalogCategory> builder)
        {
            builder.ToTable("CatalogCategories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => CatalogCategoryId.Create(v))
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(c => c.SubCategories)
                .WithOne()
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
