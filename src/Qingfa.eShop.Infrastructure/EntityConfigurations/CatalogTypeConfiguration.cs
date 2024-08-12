using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogTypes");

            // Primary key
            builder.HasKey(ct => ct.Id);

            // Properties
            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
