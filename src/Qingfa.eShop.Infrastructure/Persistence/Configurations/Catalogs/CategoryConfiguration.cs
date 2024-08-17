using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Categories;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Table Configuration

            builder.ToTable("Categories");

            #endregion

            #region Primary Key

            builder.HasKey(c => c.Id);

            #endregion

            #region Properties

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value, 
                    value => new CategoryId(value)); 

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.BannerURL)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.IncludeToStore)
                .IsRequired();

            builder.Property(c => c.ParentId)
                .HasConversion(
                    id => id!=null ? (int?)id.Value : null,
                    value => value.HasValue ? new CategoryId(value.Value) : null);

            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Complex Type Configuration

            builder.ComplexProperty(c => c.Seo, seoBuilder =>
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

            #endregion

            #region Relationships

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of parent categories with children

            // Example relationship with Product, if applicable
            // builder.HasMany(c => c.Products)
            //        .WithOne(p => p.Category)
            //        .HasForeignKey(p => p.CategoryId)
            //        .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Indexes

            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Categories_Name");

            builder.HasIndex(c => c.BannerURL)
                .HasDatabaseName("IX_Categories_BannerURL");

            #endregion
        }
    }
}
