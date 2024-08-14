using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Brands;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure the primary key
            builder.HasKey(c => c.Id);

            // Configure the properties
            builder.Property(c => c.TypeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Bio)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.IsActive)
                .IsRequired();

            // Convert CategoryId to int for storage
            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => CategoryId.Create(value)
                );

            // Configure the optional self-referencing relationship
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the table name
            builder.ToTable("Categories");
        }
    }

}
