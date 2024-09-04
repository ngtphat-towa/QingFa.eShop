using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Infrastructure.Identity.Entities.Permissions;

namespace QingFa.EShop.Infrastructure.Identity.Configurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.PermissionAction)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<PermissionAction>(v)
                );

            builder.Property(p => p.ResourceScope)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<ResourceScope>(v)
                );

            builder.Property(p => p.Description)
                .HasMaxLength(1024);

            builder.Property(p => p.Policy)
                .HasMaxLength(1024);

            // Adding indexes
            builder.HasIndex(p => p.Name);

            builder.HasIndex(p => new { p.PermissionAction, p.ResourceScope });
        }
    }
}
