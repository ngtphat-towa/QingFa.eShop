using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Infrastructure.Configurations
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            // Table configuration
            builder.ToTable("CatalogItems");

            // Key configuration
            builder.HasKey(ci => ci.Id);

            // Property configurations
            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ci => ci.Description)
                .HasMaxLength(1000);

            builder.Property(ci => ci.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ci => ci.Season)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Season)Enum.Parse(typeof(Season), v));

            // Enum conversion
            builder.Property(ci => ci.Material)
                .HasConversion(
                    v => v.ToString(),
                    v => (Material)Enum.Parse(typeof(Material), v));

            // Relationships
            builder.HasOne(ci => ci.Color)
                .WithMany()
                .HasForeignKey("ColorId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Material)
                .WithMany()
                .HasForeignKey("MaterialId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Size)
                .WithMany()
                .HasForeignKey("SizeId")
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(ci => ci.Season)
            .IsRequired()
            .HasConversion(
        v => v?.ToString() ?? string.Empty, // Ensures v is not null
        v => (Season)Enum.Parse(typeof(Season), v ?? throw new ArgumentNullException(nameof(v))) // Throws exception if v is null
    );

            builder.HasMany(ci => ci.Tags)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CatalogItemTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<CatalogItem>().WithMany().HasForeignKey("CatalogItemId"))
                .HasKey("CatalogItemId", "TagId");

            builder.HasOne(ci => ci.AgeGroup)
                .WithMany()
                .HasForeignKey("AgeGroupId")
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data configuration (optional)
            builder.HasData(
                // Example seed data
                new CatalogItem
                {
                    Id = new CatalogItemId(Guid.NewGuid()),
                    Name = "Sample Item",
                    Description = "Sample description",
                    Price = 19.99m,
                    Season = Season.Summer,
                    // Set other properties and relations as needed
                }
            );
        }
    }
}
