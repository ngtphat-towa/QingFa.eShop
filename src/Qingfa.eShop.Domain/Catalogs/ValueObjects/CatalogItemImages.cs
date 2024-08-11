using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CatalogItemImages : Entity<long>
    {
        public CatalogItemId CatalogItemId { get; private set; }
        public IReadOnlyList<string> MainImageUrls { get; private set; }
        public IReadOnlyList<string> AdditionalImageUrls { get; private set; }

        // Private Constructor
        private CatalogItemImages(
            long id,
            CatalogItemId catalogItemId,
            IEnumerable<string> mainImageUrls,
            IEnumerable<string>? additionalImageUrls = null)
            : base(id)
        {
            CatalogItemId = catalogItemId ?? throw new ArgumentNullException(nameof(catalogItemId));
            MainImageUrls = mainImageUrls.ToList().AsReadOnly();
            AdditionalImageUrls = additionalImageUrls?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly(); // Default to empty list if null
        }

        // Static Factory Method for Full Initialization
        public static CatalogItemImages Create(
            long id,
            CatalogItemId catalogItemId,
            IEnumerable<string> mainImageUrls,
            IEnumerable<string>? additionalImageUrls = null)
        {
            return new CatalogItemImages(id, catalogItemId, mainImageUrls, additionalImageUrls);
        }

        // Static Factory Method for Default Initialization
        public static CatalogItemImages CreateDefault(long id, CatalogItemId catalogItemId)
        {
            return new CatalogItemImages(
                id,
                catalogItemId,
                mainImageUrls: new List<string>(),
                additionalImageUrls: new List<string>());
        }

        // Methods to Update Image URLs
        public void AddMainImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));
            var mainImagesList = MainImageUrls.ToList();
            mainImagesList.Add(imageUrl);
            MainImageUrls = mainImagesList.AsReadOnly();
        }

        public void AddAdditionalImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));
            var additionalImagesList = AdditionalImageUrls.ToList();
            additionalImagesList.Add(imageUrl);
            AdditionalImageUrls = additionalImagesList.AsReadOnly();
        }

        public void RemoveMainImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));
            var mainImagesList = MainImageUrls.ToList();
            if (!mainImagesList.Remove(imageUrl))
            {
                throw new InvalidOperationException("Image URL not found in the main images.");
            }
            MainImageUrls = mainImagesList.AsReadOnly();
        }

        public void RemoveAdditionalImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));
            var additionalImagesList = AdditionalImageUrls.ToList();
            if (!additionalImagesList.Remove(imageUrl))
            {
                throw new InvalidOperationException("Image URL not found in the additional images.");
            }
            AdditionalImageUrls = additionalImagesList.AsReadOnly();
        }

        public void ClearMainImageUrls()
        {
            MainImageUrls = new List<string>().AsReadOnly();
        }

        public void ClearAdditionalImageUrls()
        {
            AdditionalImageUrls = new List<string>().AsReadOnly();
        }
    }
}
