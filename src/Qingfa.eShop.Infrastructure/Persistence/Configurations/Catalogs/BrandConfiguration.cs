using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            #region Table Configuration

            builder.ToTable("Brands");

            #endregion

            #region Primary Key

            builder.HasKey(b => b.Id);

            #endregion

            #region Properties

            builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new BrandId(value));

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(b => b.LogoUrl)
                .IsRequired()
                .HasMaxLength(500);

            #endregion

            #region Complex Type Configuration

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

            #endregion

            #region Indexes

            builder.HasIndex(b => b.Name)
                .HasDatabaseName("IX_Brands_Name");

            builder.HasIndex(b => b.LogoUrl)
                .HasDatabaseName("IX_Brands_LogoUrl");

            #endregion

            #region Concurrency Control

            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion
        }
    }

}
