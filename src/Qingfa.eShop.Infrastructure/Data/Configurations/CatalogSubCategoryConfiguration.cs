using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class CatalogSubCategoryConfiguration : IEntityTypeConfiguration<CatalogSubCategory>
    {
        public void Configure(EntityTypeBuilder<CatalogSubCategory> builder)
        {
            builder.ToTable("CatalogSubCategories");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => CatalogSubCategoryId.Create(v))
                .ValueGeneratedNever();

            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sc => sc.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sc => sc.ParentCategoryId)
                .IsRequired()
                .HasColumnName("ParentCategoryId");

            builder.HasOne<CatalogCategory>()
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
