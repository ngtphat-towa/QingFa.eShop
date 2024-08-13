using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;

namespace QingFa.EShop.Infrastructure.Configurations
{
    public class StyleImageConfiguration : IEntityTypeConfiguration<StyleImage>
    {
        public void Configure(EntityTypeBuilder<StyleImage> builder)
        {
            builder.ToTable("StyleImage");

            builder.HasKey(styleImage => styleImage.Id);

            builder.Property(styleImage => styleImage.SizeRepresentationUrl)
                .IsRequired()
                .HasMaxLength(2048);

            builder.HasOne(styleImage => styleImage.DefaultImage)
                .WithMany() 
                .HasForeignKey("DefaultImageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(styleImage => styleImage.SearchImage)
                .WithMany() 
                .HasForeignKey("SearchImageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(styleImage => styleImage.BackImage)
                .WithMany() 
                .HasForeignKey("BackImageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(styleImage => styleImage.FrontImage)
                .WithMany() 
                .HasForeignKey("FrontImageId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(styleImage => styleImage.RightImage)
                .WithMany() 
                .HasForeignKey("RightImageId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
