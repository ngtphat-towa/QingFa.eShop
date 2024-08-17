using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Aggregates;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.Id);

            builder.Property(c => c.Id)
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

            // Index on Name to speed up searches by name
            builder.HasIndex(b => b.Name)
                .HasDatabaseName("IX_Brands_Name");

            // Index on LogoUrl if it’s frequently queried
            builder.HasIndex(b => b.LogoUrl)
                .HasDatabaseName("IX_Brands_LogoUrl");

            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

        }
    }

}
