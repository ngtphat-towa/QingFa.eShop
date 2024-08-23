using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Core.Enums;

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

            // Configure the SEO metadata as a complex type
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

            // Configure the LogoUrl property
            builder.Property(b => b.LogoUrl)
                .HasMaxLength(1000);

            builder.Property(b => b.Status)
            .HasConversion(
                v => (int)v, 
                v => (EntityStatus)v)
            .IsRequired();

            // Add index on Name for faster lookups
            builder.HasIndex(a => a.Status);
            builder.HasIndex(b => b.Name);
            builder.HasIndex(a => a.Created);
            builder.HasIndex(a => a.CreatedBy);
            builder.HasIndex(a => a.LastModified);
            builder.HasIndex(a => a.LastModifiedBy);
        }
    }
}
