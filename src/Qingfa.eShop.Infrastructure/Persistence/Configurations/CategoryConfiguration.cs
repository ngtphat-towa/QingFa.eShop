using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;

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

            // Configure the many-to-many relationship with Product
            builder.HasMany(c => c.CategoryProducts)
                .WithOne(cp => cp.Category)
                .HasForeignKey(cp => cp.CategoryId);

            // Add index on Name for faster lookups
            builder.HasIndex(c => c.Name);
            builder.HasIndex(c => c.ParentCategoryId);
        }
    }
}
