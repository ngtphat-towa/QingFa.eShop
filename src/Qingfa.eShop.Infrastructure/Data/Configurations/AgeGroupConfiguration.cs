using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class AgeGroupConfiguration : IEntityTypeConfiguration<AgeGroup>
    {
        public void Configure(EntityTypeBuilder<AgeGroup> builder)
        {
            builder.ToTable("AgeGroups");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => AgeGroupId.Create(v))
                .ValueGeneratedNever();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
