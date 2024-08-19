using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        }
    }
}
