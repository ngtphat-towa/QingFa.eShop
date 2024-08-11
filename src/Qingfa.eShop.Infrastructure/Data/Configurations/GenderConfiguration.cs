using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => GenderId.Create(v))
                .ValueGeneratedNever();

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Description)
                .HasMaxLength(500);
        }
    }
}
