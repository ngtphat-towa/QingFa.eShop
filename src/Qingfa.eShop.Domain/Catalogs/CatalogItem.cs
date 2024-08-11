using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Bases;

public class CatalogItem : AggregateRoot<CatalogItemId>
{
    // Basic Information
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string LongDescription { get; private set; }

    // Identifiers
    public CatalogCategoryId? CatalogCategoryId { get; private set; }
    public CatalogTypeId? CatalogTypeId { get; private set; }
    public CatalogSubCategoryId? SubCategoryId { get; private set; }
    public CatalogBrandId? CatalogBrandId { get; private set; }

    // Metadata Information
    public Material Material { get; private set; }
    public Season Season { get; private set; }
    public Gender Gender { get; private set; }
    public AgeGroup AgeGroup { get; private set; }
    public CareInstructions CareInstructions { get; private set; }

    public Price Price { get; private set; }
    public IReadOnlyCollection<Size> SizeOptions { get; private set; }
    public IReadOnlyCollection<Color> ColorOptions { get; private set; }
    public CatalogItemInventory Inventory { get; private set; }
    public CatalogItemImages Images { get; private set; }

    // Additional Information
    public Rating Rating { get; private set; }
    public int ReviewCount { get; private set; }
    public IReadOnlyList<Tag> Tags { get; private set; }
    public bool Active { get; private set; }

    // Private Constructor
    private CatalogItem(
        CatalogItemId id,
        string name,
        string description,
        string longDescription,
        CatalogCategoryId? catalogCategoryId,
        CatalogTypeId? catalogTypeId,
        CatalogSubCategoryId? subCategoryId,
        CatalogBrandId? catalogBrandId,
        Material material,
        Season season,
        Gender gender,
        AgeGroup ageGroup,
        CareInstructions careInstructions,
        Price price,
        IReadOnlyCollection<Size> sizeOptions,
        IReadOnlyCollection<Color> colorOptions,
        CatalogItemInventory inventory,
        CatalogItemImages images,
        Rating rating,
        int reviewCount,
        IReadOnlyList<Tag> tags,
        bool active)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        LongDescription = longDescription ?? throw new ArgumentNullException(nameof(longDescription));
        CatalogCategoryId = catalogCategoryId;
        CatalogTypeId = catalogTypeId;
        SubCategoryId = subCategoryId;
        CatalogBrandId = catalogBrandId;
        Material = material;
        Season = season;
        Gender = gender;
        AgeGroup = ageGroup;
        CareInstructions = careInstructions;
        Price = price;
        SizeOptions = sizeOptions ?? new List<Size>().AsReadOnly();
        ColorOptions = colorOptions ?? new List<Color>().AsReadOnly();
        Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        Images = images ?? throw new ArgumentNullException(nameof(images));
        Rating = rating;
        ReviewCount = reviewCount;
        Tags = tags ?? new List<Tag>().AsReadOnly();
        Active = active;
        UpdatedTime = DateTime.UtcNow;
    }

    // Factory Method for Full Initialization
    public static CatalogItem Create(
        CatalogItemId id,
        string name,
        string description,
        string longDescription,
        CatalogCategoryId? catalogCategoryId,
        CatalogTypeId? catalogTypeId,
        CatalogSubCategoryId? subCategoryId,
        CatalogBrandId? catalogBrandId,
        Material material,
        Season season,
        Gender gender,
        AgeGroup ageGroup,
        CareInstructions careInstructions,
        Price price,
        IReadOnlyCollection<Size> sizeOptions,
        IReadOnlyCollection<Color> colorOptions,
        CatalogItemInventory inventory,
        CatalogItemImages images,
        Rating rating,
        int reviewCount,
        IReadOnlyList<Tag> tags,
        bool active)
    {
        return new CatalogItem(
            id,
            name,
            description,
            longDescription,
            catalogCategoryId,
            catalogTypeId,
            subCategoryId,
            catalogBrandId,
            material,
            season,
            gender,
            ageGroup,
            careInstructions,
            price,
            sizeOptions,
            colorOptions,
            inventory,
            images,
            rating,
            reviewCount,
            tags,
            active);
    }

    // Update Methods
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        Name = name;
        UpdatedTime = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
        UpdatedTime = DateTime.UtcNow;
    }

    // Other update methods omitted for brevity
}

