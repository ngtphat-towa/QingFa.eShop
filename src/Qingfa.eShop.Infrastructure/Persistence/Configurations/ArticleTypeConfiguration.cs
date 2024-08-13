using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal class ArticleTypeConfiguration : IEntityTypeConfiguration<ArticleType>
    {
        public void Configure(EntityTypeBuilder<ArticleType> builder)
        {
            // Configure the table name
            builder.ToTable("ArticleTypes");

            // Configure the primary key
            builder.HasKey(at => at.Id);

            // Configure value object conversion
            builder.Property(at => at.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ArticleTypeId.Create(value));

            // Configure properties
            builder.Property(at => at.TypeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(at => at.Active)
                .IsRequired();

            builder.Property(at => at.SocialSharingEnabled)
                .IsRequired();

            builder.Property(at => at.IsReturnable)
                .IsRequired();

            builder.Property(at => at.IsExchangeable)
                .IsRequired();

            builder.Property(at => at.PickupEnabled)
                .IsRequired();

            builder.Property(at => at.IsTryAndBuyEnabled)
                .IsRequired();

            // Configure value object conversion for ServiceabilityDisclaimer
            builder.OwnsOne(at => at.ServiceabilityDisclaimer, sd =>
            {
                sd.Property(s => s.Title)
                    .IsRequired()
                    .HasMaxLength(200); // Adjust length as needed

                sd.Property(s => s.Desc)
                    .HasMaxLength(1000); // Adjust length as needed
            });
        }
    }
}
