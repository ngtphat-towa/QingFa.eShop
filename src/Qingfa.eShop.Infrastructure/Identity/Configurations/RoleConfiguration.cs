using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Infrastructure.Identity.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace QingFa.EShop.Infrastructure.Identity.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Description)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId);

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<IdentityUserRole<Guid>>();
        }
    }
}
