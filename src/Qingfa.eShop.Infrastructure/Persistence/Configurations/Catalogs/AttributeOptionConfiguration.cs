using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Attributes;
using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class AttributeOptionConfiguration : IEntityTypeConfiguration<AttributeOption>
    {
        public void Configure(EntityTypeBuilder<AttributeOption> builder)
        {
            #region Table Configuration

            builder.ToTable("AttributeOptions");

            #endregion

            #region Primary Key

            builder.HasKey(o => o.Id);

            #endregion

            #region Properties

            // Configure the property as a GUID column for strong typing
            builder.Property(o => o.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,  
                    value => new AttributeOptionId(value)  
                );

            // Configure the property as a GUID column for strong typing
            builder.Property(o => o.AttributeId)
                .HasColumnName("AttributeId")
                .HasConversion(
                    id => id.Value, 
                    value => new AttributeId(value)  
                );

            builder.Property(o => o.OptionValue)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.IsDefault)
                .IsRequired();

            builder.Property(o => o.SortOrder)
                .IsRequired();

            // Add a timestamp for concurrency control
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Indexes

            builder.HasIndex(o => o.OptionValue)
                .HasDatabaseName("IX_AttributeOptions_OptionValue");

            #endregion
        }
    }
}
