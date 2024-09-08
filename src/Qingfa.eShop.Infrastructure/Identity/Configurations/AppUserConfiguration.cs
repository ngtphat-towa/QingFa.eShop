﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingFa.EShop.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Identity;
using QingFa.EShop.Domain.Identities.Entities;

namespace QingFa.EShop.Infrastructure.Identity.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.Property(u => u.ShippingAddress)
                .HasConversion(
                    address => address!.ToString(),
                    str => Address.FromString(str))
                .IsRequired();

            builder.Property(u => u.BillingAddress)
                .HasConversion(
                    address => address!.ToString(),
                    str => Address.FromString(str))
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<IdentityUserRole<Guid>>();
        }
    }
}