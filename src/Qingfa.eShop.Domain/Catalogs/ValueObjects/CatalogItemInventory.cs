using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Commons.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class CatalogItemInventory
    {
        public IEnumerable<Size> SizeOptions { get; }
        public IEnumerable<Color> ColorOptions { get; }
        public Price Price { get; }
        public Price? DiscountPrice { get; }
        public int StockQuantity { get; }
        public string Sku { get; }
        public IEnumerable<string> MainImageUrls { get; }

        public CatalogItemInventory(IEnumerable<Size> sizeOptions, IEnumerable<Color> colorOptions, Price price, Price? discountPrice, int stockQuantity, string sku, IEnumerable<string> mainImageUrls)
        {
            SizeOptions = sizeOptions;
            ColorOptions = colorOptions;
            Price = price;
            DiscountPrice = discountPrice;
            StockQuantity = stockQuantity;
            Sku = sku;
            MainImageUrls = mainImageUrls;
        }
    }
}
