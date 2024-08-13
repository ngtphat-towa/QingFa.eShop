using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class SizeOptionConfiguration : IEntityTypeConfiguration<SizeOption>
    {
        public void Configure(EntityTypeBuilder<SizeOption> builder)
        {
            builder.ToTable("SizeOptions");

            builder.HasKey(cs => cs.Id);

            // Configure value object conversion
            builder.Property(cs => cs.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SizeOptionId.Create(value));

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

            // Configure InventoryCount property
            builder.Property(cs => cs.InventoryCount)
                .IsRequired()
                .HasColumnType("int");

        }
    }
}
