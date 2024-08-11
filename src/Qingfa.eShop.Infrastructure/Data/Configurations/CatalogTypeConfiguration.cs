using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogTypes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => CatalogTypeId.Create(v))
                .ValueGeneratedNever();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
