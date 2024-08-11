using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => MaterialId.Create(v))
                .ValueGeneratedNever();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Description)
                .HasMaxLength(500);
        }
    }
}
