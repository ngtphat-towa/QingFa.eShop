using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Aggregates;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Common.Enums;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure the primary key
            builder.HasKey(c => c.Id);

            // Configure the properties
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.BannerURL)
                .IsRequired()
                .HasMaxLength(255);

            // Configure enums to be stored as short
            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion(
                    v => (short)v,
                    v => (EntityStatus)v);

            builder.Property(c => c.IncludeToStore)
                .IsRequired();

            // Configure value object
            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value));

            // Configure one-to-many relationship with itself for ParentId
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure value object for SEO info
            builder.ComplexProperty(b => b.Seo, seoBuilder =>
            {
                seoBuilder.Property(s => s.MetaTitle)
                    .IsRequired()
                    .HasMaxLength(200);

                seoBuilder.Property(s => s.MetaDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                seoBuilder.Property(s => s.URLSlug)
                    .IsRequired()
                    .HasMaxLength(100);

                seoBuilder.Property(s => s.CanonicalURL)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            // Configure indexes
            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Category_Name");

            builder.HasIndex(c => c.Status)
                .HasDatabaseName("IX_Category_Status");

            builder.HasIndex(c => c.IncludeToStore)
                .HasDatabaseName("IX_Category_IncludeToStore");

            // Configure the table name
            builder.ToTable("Categories");
        }
    }
}
