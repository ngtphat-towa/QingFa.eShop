using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Core.Entities;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class AuditEntityConfiguration : IEntityTypeConfiguration<AuditEntity>
    {
        public void Configure(EntityTypeBuilder<AuditEntity> builder)
        {
            // Configure properties for audit information
            builder.Property(a => a.Created)
                .IsRequired();

            builder.Property(a => a.CreatedBy)
                .HasMaxLength(100); 

            builder.Property(a => a.LastModified)
                .IsRequired();

            builder.Property(a => a.LastModifiedBy)
                .HasMaxLength(100);


            builder.Property(b => b.Status)
                    .HasConversion(
                        v => (int)v,
                        v => (EntityStatus)v)
                    .IsRequired();

            builder.HasIndex(a => a.Id);
            builder.HasIndex(a => a.Created);
            builder.HasIndex(a => a.CreatedBy);
            builder.HasIndex(a => a.LastModified);
            builder.HasIndex(a => a.LastModifiedBy);
        }
    }
}
