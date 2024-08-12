using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Catalogs;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    internal sealed class CatalogProductConfigurations : IEntityTypeConfiguration<CatalogProduct>
    {
        public void Configure(EntityTypeBuilder<CatalogProduct> builder)
        {
            ConfigureCatalogProductsTable(builder);
        }

        private static void ConfigureCatalogProductsTable(EntityTypeBuilder<CatalogProduct> builder)
        {
            builder.ToTable("CatalogProducts");

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CatalogProductId.Create(value));

            builder
                .Property(p => p.Name)
                .HasMaxLength(100);

            builder
                .Property(p => p.Description)
                .HasMaxLength(500);

            builder
                .Property(p => p.Material)
                .HasConversion(
                    material => material?.Name, // Change to appropriate property or method
                    name => Material.Create(new MaterialId(Guid.NewGuid()), name, "", new MaterialCategory("", ""), "")); // Adjust as needed

            builder
                .Property(p => p.FashionYear)
                .HasConversion(
                    year => year.Year, // Assuming FashionYear is a record with Year property
                    year => FashionYear.Create(year, DateTime.Now.Year)); // Adjust conversion logic

            builder
                .Property(p => p.FashionSeason)
                .HasConversion(
                    season => season.Season, // Assuming FashionSeason is a record with Season property
                    season => new FashionSeason(season, DateTime.Now.Year)); // Adjust conversion logic

            builder
                .Property(p => p.Gender)
                .HasConversion(
                    gender => gender.ToString(),
                    str => Enum.Parse<Gender>(str))
                .HasMaxLength(50);

            builder
                .Property(p => p.Price)
                .HasConversion(
                    price => price.Amount,
                    amount => new Price(amount, "USD")); // Adjust as needed

            builder
                .Property(p => p.SKU)
                .HasConversion(
                    sku => sku?.Value, // Assuming SKU has a Value property
                    value => SKU.Create(value)); // Adjust as needed

            builder
                .Property(p => p.Status)
                .HasConversion(
                    status => status.ToString(),
                    str => Enum.Parse<ProductStatus>(str))
                .HasMaxLength(50);

            builder
                .Property(p => p.UnitOfMeasure)
                .HasConversion(
                    unit => unit? // Assuming UnitOfMeasure has a Name property
                    name => UnitOfMeasure.Create(name)); // Adjust as needed

            builder
                .Property(p => p.AdditionalCosts)
                .HasConversion(
                    costs => costs.Serialize(),
                    serialized => AdditionalCost.Deserialize(serialized));

            builder.OwnsMany(p => p.TagIds, tagBuilder =>
            {
                tagBuilder.ToTable("CatalogProductTags");
                tagBuilder
                    .WithOwner()
                    .HasForeignKey("CatalogProductId");
                tagBuilder.HasKey("Id");
                tagBuilder
                    .Property(t => t.Value)
                    .HasColumnName("TagId")
                    .ValueGeneratedNever();
                tagBuilder.OwnedEntityType.RemoveDiscriminatorValue();
            });

            builder.OwnsMany(p => p.CatalogSizeIds, sizeBuilder =>
            {
                sizeBuilder.ToTable("CatalogProductSizes");
                sizeBuilder
                    .WithOwner()
                    .HasForeignKey("CatalogProductId");
                sizeBuilder.HasKey("Id");
                sizeBuilder
                    .Property(s => s.Value)
                    .HasColumnName("SizeId")
                    .ValueGeneratedNever();
                sizeBuilder.OwnedEntityType.RemoveDiscriminatorValue();
            });

            builder.OwnsMany(p => p.CatalogColorIds, colorBuilder =>
            {
                colorBuilder.ToTable("CatalogProductColors");
                colorBuilder
                    .WithOwner()
                    .HasForeignKey("CatalogProductId");
                colorBuilder.HasKey("Id");
                colorBuilder
                    .Property(c => c.Value)
                    .HasColumnName("ColorId")
                    .ValueGeneratedNever();
                colorBuilder.OwnedEntityType.RemoveDiscriminatorValue();
            });

            builder
                .HasOne(p => p.Inventory)
                .WithMany()
                .HasForeignKey("InventoryId");
        }
    }
}
