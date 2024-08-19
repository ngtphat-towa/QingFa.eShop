using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // Configure the primary key
            builder.HasKey(b => b.Id);

            // Configure the Name property
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Description property
            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            // Configure the SEO metadata
            builder.ComplexProperty(b => b.SeoMeta, seo =>
            {
                seo.Property(s => s.Title).HasMaxLength(100).IsRequired();
                seo.Property(s => s.Description).HasMaxLength(300).IsRequired();
                seo.Property(s => s.Keywords).HasMaxLength(500).IsRequired();
                seo.Property(s => s.CanonicalUrl).HasMaxLength(1000);
                seo.Property(s => s.Robots).HasMaxLength(100);
            });

            // Configure the LogoUrl property
            builder.Property(b => b.LogoUrl)
                .HasMaxLength(1000);

            // Configure the relationship with Product
            builder.HasMany(b => b.Products)
                .WithOne()
                .HasForeignKey("BrandId");
        }
    }
}
