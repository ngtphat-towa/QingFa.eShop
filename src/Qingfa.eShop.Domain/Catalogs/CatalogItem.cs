using QingFa.EShop.Domain.Catalogs.Entities;
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

    // Metadata
    public CatalogItemMetadata Metadata { get; private set; }

    // Inventory Information
    public CatalogItemInventory Inventory { get; private set; }

    // Images
    public CatalogItemImages Images { get; private set; }

    // Additional Information
    public Rating Rating { get; private set; }
    public int ReviewCount { get; private set; }
    public IEnumerable<Tag> Tags { get; private set; }
    public bool Active { get; private set; }

    // Constructor
    private CatalogItem(
        CatalogItemId id,
        string name,
        string description,
        string longDescription,
        CatalogCategoryId? catalogCategoryId,
        CatalogTypeId? catalogTypeId,
        CatalogSubCategoryId? subCategoryId,
        CatalogBrandId? catalogBrandId,
        CatalogItemMetadata metadata,
        CatalogItemInventory inventory,
        CatalogItemImages images,
        Rating rating,
        int reviewCount,
        IEnumerable<Tag> tags,
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
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        Images = images ?? throw new ArgumentNullException(nameof(images));
        Rating = rating ?? Rating.Default; 
        ReviewCount = reviewCount;
        Tags = tags ?? new List<Tag>();
        Active = active;
    }

    // Factory Method
    public static CatalogItem Create(
        CatalogItemId id,
        string name,
        string description,
        string longDescription,
        CatalogCategoryId? catalogCategoryId,
        CatalogTypeId? catalogTypeId,
        CatalogSubCategoryId? subCategoryId,
        CatalogBrandId? catalogBrandId,
        CatalogItemMetadata metadata,
        CatalogItemInventory inventory,
        CatalogItemImages images,
        Rating rating,
        int reviewCount,
        IEnumerable<Tag> tags,
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
            metadata,
            inventory,
            images,
            rating,
            reviewCount,
            tags,
            active);
    }

    // Methods to Update Attributes
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

    public void UpdateLongDescription(string longDescription)
    {
        LongDescription = longDescription ?? throw new ArgumentNullException(nameof(longDescription));
        UpdatedTime = DateTime.UtcNow;
    }

    public void UpdateMetadata(CatalogItemMetadata metadata)
    {
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        UpdatedTime = DateTime.UtcNow;
    }

    public void UpdateInventory(CatalogItemInventory inventory)
    {
        Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        UpdatedTime = DateTime.UtcNow;
    }

    public void UpdateImages(CatalogItemImages images)
    {
        Images = images ?? throw new ArgumentNullException(nameof(images));
        UpdatedTime = DateTime.UtcNow;
    }

    public void UpdateTags(IEnumerable<Tag> tags)
    {
        Tags = tags ?? throw new ArgumentNullException(nameof(tags));
        UpdatedTime = DateTime.UtcNow;
    }

    public void SetActive(bool active)
    {
        Active = active;
        UpdatedTime = DateTime.UtcNow;

        // Optional: Add domain event for activation status change
        // AddDomainEvent(new CatalogItemStatusChangedEvent(Id, active));
    }
}
