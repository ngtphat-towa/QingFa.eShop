using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Commons.ValueObjects;

public class CatalogItemInventory : Entity<long>
{
    // Properties
    public CatalogItemId CatalogItemId { get; private set; }
    public Price? Price { get; private set; }
    public Price? DiscountPrice { get; private set; }
    public int StockQuantity { get; private set; }
    public string Sku { get; private set; }

    // Private Constructor
    private CatalogItemInventory(
        CatalogItemId catalogItemId,
        Price price,
        Price? discountPrice,
        int stockQuantity,
        string sku)
        : base(default(long))
    {
        CatalogItemId = catalogItemId;
        Price = price ?? Price.CreateDefault();
        DiscountPrice = discountPrice;
        StockQuantity = stockQuantity;
        Sku = sku ?? string.Empty;
    }

    // Static Factory Method for Full Initialization
    public static CatalogItemInventory Create(
        CatalogItemId catalogItemId,
        Price price,
        Price? discountPrice,
        int stockQuantity,
        string sku,
        IEnumerable<string> mainImageUrls)
    {
        return new CatalogItemInventory(
            catalogItemId,
            price,
            discountPrice,
            stockQuantity,
            sku);
    }

    // Static Factory Method for Default Initialization
    public static CatalogItemInventory CreateWithDefaults(CatalogItemId catalogItemId)
    {
        return new CatalogItemInventory(
            catalogItemId,
            price: Price.CreateDefault(),
            discountPrice: null,
            stockQuantity: 0,
            sku: string.Empty);
    }

    public void UpdatePrice(Price price)
    {
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }

    public void UpdateDiscountPrice(Price? discountPrice)
    {
        DiscountPrice = discountPrice;
    }

    public void UpdateStockQuantity(int stockQuantity)
    {
        StockQuantity = stockQuantity;
    }

    public void UpdateSku(string sku)
    {
        Sku = sku ?? string.Empty;
    }
}
