using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Sizes");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => SizeId.Create(v))
                .ValueGeneratedNever();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.SizeChartURL)
                .HasMaxLength(1000);

            builder.Property(s => s.SizeType)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
