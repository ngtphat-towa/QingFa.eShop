using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Colors");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => ColorId.Create(v))
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.HexCode)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(c => c.Description)
                .HasMaxLength(500);
        }
    }
}
