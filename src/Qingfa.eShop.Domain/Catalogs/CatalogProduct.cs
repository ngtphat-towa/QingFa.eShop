using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Catalogs;
using QingFa.EShop.Domain.DomainModels;

public class CatalogProduct : AggregateRoot<CatalogProductId>
{
    private readonly List<CatalogCategoryId> _catalogCategoryIds = new();
    private readonly List<CatalogColorId> _catalogColorIds = new();
    private readonly List<CatalogSizeId> _catalogSizeIds = new();
    private readonly List<TagId> _tagIds = new();

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public FashionYear FashionYear { get; private set; }
    public FashionSeason FashionSeason { get; private set; }

    // Navigation properties
    public CatalogTypeId? TypeId { get; private set; }
    public CatalogBrandId? BrandId { get; private set; }
    public IReadOnlyList<CatalogCategoryId> CatalogCategoryIds => _catalogCategoryIds.AsReadOnly();
    public IReadOnlyList<CatalogColorId> CatalogColorIds => _catalogColorIds.AsReadOnly();
    public IReadOnlyList<CatalogSizeId> CatalogSizeIds => _catalogSizeIds.AsReadOnly();
    public IReadOnlyList<TagId> TagIds => _tagIds.AsReadOnly();

    public Gender Gender { get; private set; }
    public Price Price { get; private set; }
    public SKU? SKU { get; private set; }
    public InventoryId? InventoryId { get; private set; }
    public ProductStatus Status { get; private set; }
    public UnitOfMeasure? UnitOfMeasure { get; private set; }
    public AdditionalCost? AdditionalCosts { get; private set; }

    private CatalogProduct(
        CatalogProductId catalogProductId,
        string name,
        string description,
        string longDescription,
        FashionYear fashionYear,
        FashionSeason fashionSeason,
        IReadOnlyCollection<CatalogSizeId> catalogSizeIds,
        IReadOnlyCollection<CatalogColorId> catalogColorIds,
        IReadOnlyCollection<TagId> tagIds,
        CatalogCategoryId? categoryId,
        CatalogTypeId? typeId,
        CatalogBrandId? brandId,
        Gender gender,
        Price price,
        SKU? sku,
        InventoryId? inventoryId,
        ProductStatus status,
        UnitOfMeasure? unitOfMeasure,
        AdditionalCost? additionalCosts)
        : base(catalogProductId)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        Description = description;
        LongDescription = longDescription;
        FashionYear = fashionYear ?? throw new ArgumentNullException(nameof(fashionYear));
        FashionSeason = fashionSeason ?? throw new ArgumentNullException(nameof(fashionSeason));
        _catalogSizeIds.AddRange(catalogSizeIds ?? throw new ArgumentNullException(nameof(catalogSizeIds)));
        _catalogColorIds.AddRange(catalogColorIds ?? throw new ArgumentNullException(nameof(catalogColorIds)));
        _tagIds.AddRange(tagIds ?? throw new ArgumentNullException(nameof(tagIds)));
        CategoryId = categoryId;
        TypeId = typeId;
        BrandId = brandId;
        Gender = gender;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        SKU = sku;
        InventoryId = inventoryId;
        Status = status;
        UnitOfMeasure = unitOfMeasure;
        AdditionalCosts = additionalCosts;
    }

    public static CatalogProduct Create(
        CatalogProductId catalogProductId,
        string name,
        string description,
        string longDescription,
        FashionYear fashionYear,
        FashionSeason fashionSeason,
        IReadOnlyCollection<CatalogSizeId> catalogSizeIds,
        IReadOnlyCollection<CatalogColorId> catalogColorIds,
        IReadOnlyCollection<TagId> tagIds,
        CatalogCategoryId? categoryId,
        CatalogTypeId? typeId,
        CatalogBrandId? brandId,
        Gender gender,
        Price price,
        SKU? sku,
        InventoryId? inventoryId,
        ProductStatus status,
        UnitOfMeasure? unitOfMeasure,
        AdditionalCost? additionalCosts)
    {
        return new CatalogProduct(
            catalogProductId,
            name,
            description,
            longDescription,
            fashionYear,
            fashionSeason,
            catalogSizeIds,
            catalogColorIds,
            tagIds,
            categoryId,
            typeId,
            brandId,
            gender,
            price,
            sku,
            inventoryId,
            status,
            unitOfMeasure,
            additionalCosts);
    }

    public void UpdatePrice(Price newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void UpdateInventory(InventoryId? newInventoryId)
    {
        InventoryId = newInventoryId;
    }

    public void UpdateUnitOfMeasure(UnitOfMeasure? newUnitOfMeasure)
    {
        UnitOfMeasure = newUnitOfMeasure;
    }

    public void UpdateStatus(ProductStatus newStatus)
    {
        Status = newStatus;
    }

    public void UpdateAdditionalCosts(AdditionalCost? newAdditionalCosts)
    {
        AdditionalCosts = newAdditionalCosts;
    }

    public decimal CalculateFinalPrice()
    {
        if (Price == null) throw new InvalidOperationException("Price is not set.");

        var finalPrice = Price.Amount;

        // Apply additional costs based on size
        foreach (var sizeId in CatalogSizeIds)
        {
            var sizeAdjustment = AdditionalCosts?.GetAdjustment(AttributeType.Size, sizeId) ?? 0;
            finalPrice += sizeAdjustment;
        }

        // Apply additional costs based on color
        foreach (var colorId in CatalogColorIds)
        {
            var colorAdjustment = AdditionalCosts?.GetAdjustment(AttributeType.Color, colorId) ?? 0;
            finalPrice += colorAdjustment;
        }

        // Apply additional costs based on material
        var materialAdjustment = AdditionalCosts?.GetAdjustment(AttributeType.Material, Material!) ?? 0;
        finalPrice += materialAdjustment;

        // Apply additional costs based on fashion season
        var fashionSeasonAdjustment = AdditionalCosts?.GetAdjustment(AttributeType.FashionSeason, FashionSeason) ?? 0;
        finalPrice += fashionSeasonAdjustment;

        // Apply additional costs based on tags
        foreach (var tagId in TagIds)
        {
            var tagAdjustment = AdditionalCosts?.GetAdjustment(AttributeType.Tag, tagId) ?? 0;
            finalPrice += tagAdjustment;
        }

        return finalPrice;
    }

    // Parameterless constructor for ORM frameworks
    protected CatalogProduct() : base(default!)
    {
    }
}
