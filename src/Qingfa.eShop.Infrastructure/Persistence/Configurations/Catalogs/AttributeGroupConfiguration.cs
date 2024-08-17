using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class AttributeGroupConfiguration : IEntityTypeConfiguration<AttributeGroup>
    {
        public void Configure(EntityTypeBuilder<AttributeGroup> builder)
        {
            #region Table Configuration

            builder.ToTable("AttributeGroups");

            #endregion

            #region Primary Key

            builder.HasKey(g => g.Id);

            #endregion

            #region Properties

            // Configure the property as a GUID column for strong typing
            builder.Property(g => g.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value, 
                    value => new AttributeGroupId(value) 
                );

            builder.Property(g => g.GroupName)
                .IsRequired()
                .HasMaxLength(100);

            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Relationships

            // Optionally configure the relationship to VariantAttribute
            builder.HasMany<Domain.Catalogs.Attributes.ProductAttribute>()
                .WithOne()
                .HasForeignKey(v => v.AttributeGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Indexes

            builder.HasIndex(g => g.GroupName)
                .HasDatabaseName("IX_AttributeGroups_GroupName");

            #endregion
        }
    }
}
