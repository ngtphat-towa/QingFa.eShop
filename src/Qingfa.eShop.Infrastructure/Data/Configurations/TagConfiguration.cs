using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.Value,
                    v => TagId.Create(v))
                .ValueGeneratedNever();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
