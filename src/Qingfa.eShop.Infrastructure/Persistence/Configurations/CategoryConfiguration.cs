using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure the primary key
            builder.HasKey(c => c.Id);

            // Configure the Name property
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Description property
            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            // Configure the ImageUrl property
            builder.Property(c => c.ImageUrl)
                .HasMaxLength(1000);

            // Configure the ParentCategoryId property
            builder.Property(c => c.ParentCategoryId)
                .IsRequired(false);

            // Configure self-referencing relationship for ParentCategory
            builder.HasOne(c => c.ParentCategory)
                .WithMany(p => p.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the many-to-many relationship with CategoryProduct
            builder.HasMany(c => c.CategoryProducts)
                .WithOne(cp => cp.Category)
                .HasForeignKey(cp => cp.CategoryId);

            builder.Property(b => b.Status)
            .HasConversion(
                v => (int)v,
                v => (EntityStatus)v)
            .IsRequired();

            // Add indexes for faster lookups
            builder.HasIndex(c => c.Name);
            builder.HasIndex(c => c.ParentCategoryId);

            // Configure SeoMeta as a complex type if applicable
            builder.OwnsOne(c => c.SeoMeta, seo =>
            {
                seo.Property(s => s.Title).HasMaxLength(100).IsRequired();
                seo.Property(s => s.Description).HasMaxLength(300).IsRequired();
                seo.Property(s => s.Keywords).HasMaxLength(500).IsRequired();
                seo.Property(s => s.CanonicalUrl).HasMaxLength(1000);
                seo.Property(s => s.Robots).HasMaxLength(100);

                seo.HasIndex(s => s.Title);
                seo.HasIndex(s => s.Description);
                seo.HasIndex(s => s.Keywords);
            });
        }
    }
}
