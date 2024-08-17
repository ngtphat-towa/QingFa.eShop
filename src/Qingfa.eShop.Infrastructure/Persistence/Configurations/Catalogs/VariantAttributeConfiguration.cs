using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class VariantAttributeConfiguration : IEntityTypeConfiguration<VariantAttribute>
    {
        public void Configure(EntityTypeBuilder<VariantAttribute> builder)
        {
            #region Table Configuration

            builder.ToTable("VariantAttributes");

            #endregion

            #region Primary Key

            builder.HasKey(a => a.Id);

            #endregion

            #region Properties

            // Configure the property as a GUID column for strong typing
            builder.Property(a => a.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,  // Convert from AttributeId to GUID
                    value => new AttributeId(value)  // Convert from GUID to AttributeId
                );

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.AttributeCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Type)
                .IsRequired();

            builder.Property(a => a.IsRequired)
                .IsRequired();

            builder.Property(a => a.IsFilterable)
                .IsRequired();

            builder.Property(a => a.ShowToCustomers)
                .IsRequired();

            builder.Property(a => a.SortOrder)
                .IsRequired();

            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Relationships

            // Configure the relationship to AttributeOption
            builder.HasMany(a => a.Options)
                .WithOne()
                .HasForeignKey("AttributeId")  
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Indexes

            builder.HasIndex(a => a.AttributeCode)
                .HasDatabaseName("IX_VariantAttributes_AttributeCode");

            #endregion
        }
    }
}
