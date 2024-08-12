using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CatalogSizeConfiguration : IEntityTypeConfiguration<CatalogSize>
    {
        public void Configure(EntityTypeBuilder<CatalogSize> builder)
        {
            builder.ToTable("CatalogSizes");


            builder.HasKey(cs => cs.Id);
            // Configure value object conversion
            builder.Property(cs => cs.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CatalogSizeId.Create(value));

            // Configure properties
            builder.Property(cs => cs.SizeLabel)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cs => cs.MinimumValue)
                .HasColumnType("int");

            builder.Property(cs => cs.MaximumValue)
                .HasColumnType("int");

            builder.Property(cs => cs.Unit)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
