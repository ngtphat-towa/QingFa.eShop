using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Images.ValueObjects;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    public class ImageDataConfiguration : IEntityTypeConfiguration<ImageData>
    {
        public void Configure(EntityTypeBuilder<ImageData> builder)
        {
            // Define the table name
            builder.ToTable("ImageData");

            // Define the primary key
            builder.HasKey(imageData => imageData.Id);

            // Define properties and their configurations
            builder.Property(imageData => imageData.ImageUrl)
                .IsRequired()
                .HasMaxLength(2048); // Assuming a max length for URLs

            builder.Property(imageData => imageData.ImageType)
                .IsRequired()
                .HasMaxLength(50); // Adjust based on expected length of image types

            // Configure the Resolutions property as a JSON column
            builder.Property(imageData => imageData.Resolutions)
                .HasConversion(
                    // Serialize the Dictionary<key, Resolution> to a JSON string
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    // Deserialize the JSON string back to Dictionary<key, Resolution>
                    v => JsonSerializer.Deserialize<Dictionary<string, Resolution>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) ?? new Dictionary<string, Resolution>())
                .HasColumnType("jsonb"); // Use "jsonb" for PostgreSQL; adjust for other databases

            // Optionally, configure additional settings if needed
            // For example, add a unique index on the ImageUrl if required
            // builder.HasIndex(imageData => imageData.ImageUrl).IsUnique();
        }
    }
}
