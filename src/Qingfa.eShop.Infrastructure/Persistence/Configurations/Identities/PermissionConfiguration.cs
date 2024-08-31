using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Common.Enums;
using QingFa.EShop.Infrastructure.Identity.Entities.Permissions;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Identities
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Action)
                .IsRequired()
                .HasConversion(
                    action => action.Id,
                    id => PermissionAction.FromId(id)!);

            builder.Property(p => p.Resource)
                .IsRequired()
                .HasConversion(
                    resource => resource.Id,
                    id => ResourceScope.FromId(id)!);

            builder.Property(p => p.Description)
                .HasMaxLength(1024);
        }
    }
}