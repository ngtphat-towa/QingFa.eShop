using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    public class ExampleMetaConfiguration : IEntityTypeConfiguration<ExampleMeta>
    {
        public void Configure(EntityTypeBuilder<ExampleMeta> builder)
        {
            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.Created)
                   .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .HasMaxLength(255);

            builder.Property(e => e.LastModified)
                   .IsRequired();

            builder.Property(e => e.LastModifiedBy)
                   .HasMaxLength(255);

            builder.Property(b => b.Status)
                    .HasConversion(
                        v => (int)v,
                        v => (EntityStatus)v)
                    .IsRequired();

            builder.HasIndex(b => b.Name);
            builder.HasIndex(b => b.Status);
            builder.HasIndex(a => a.Created);
            builder.HasIndex(a => a.CreatedBy);
            builder.HasIndex(a => a.LastModified);
            builder.HasIndex(a => a.LastModifiedBy);

        }
    }
}
