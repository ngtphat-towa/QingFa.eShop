using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QingFa.EShop.Domain.Catalogs.Attributes;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations.Catalogs
{
    public class AttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            #region Table Configuration

            builder.ToTable("Attributes");

            #endregion

            #region Primary Key

            builder.HasKey(a => a.Id);

            #endregion

            #region Properties

            builder.Property(a => a.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => new ProductAttributeId(value)
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

            builder.Property(a => a.AttributeGroupId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new AttributeGroupId(value)
                );

            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();

            #endregion

            #region Relationships

            builder.HasMany(a => a.Options)
                .WithOne() 
                .HasForeignKey("AttributeId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<AttributeGroup>()
                .WithMany() 
                .HasForeignKey(a => a.AttributeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Indexes

            builder.HasIndex(a => a.AttributeCode)
                .HasDatabaseName("IX_Attributes_AttributeCode");

            #endregion
        }
    }
}
