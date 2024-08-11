namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents the images associated with a catalog item.
    /// </summary>
    public class CatalogItemImages
    {
        /// <summary>
        /// Gets the URLs of the main images for the catalog item.
        /// </summary>
        public IEnumerable<string> MainImageUrls { get; private set; }

        /// <summary>
        /// Gets the URLs of additional images for the catalog item (e.g., close-ups, alternate views).
        /// </summary>
        public IEnumerable<string> AdditionalImageUrls { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogItemImages"/> class.
        /// </summary>
        /// <param name="mainImageUrls">The URLs of the main images for the catalog item.</param>
        /// <param name="additionalImageUrls">The URLs of additional images for the catalog item.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mainImageUrls"/> is null.</exception>
        public CatalogItemImages(IEnumerable<string> mainImageUrls, IEnumerable<string>? additionalImageUrls = null)
        {
            MainImageUrls = mainImageUrls ?? throw new ArgumentNullException(nameof(mainImageUrls));
            AdditionalImageUrls = additionalImageUrls ?? new List<string>(); // Default to empty list if null
        }

        /// <summary>
        /// Adds a main image URL to the catalog item.
        /// </summary>
        /// <param name="imageUrl">The URL of the main image to add.</param>
        public void AddMainImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));

            var imageUrls = new List<string>(MainImageUrls) { imageUrl };
            MainImageUrls = imageUrls;
        }

        /// <summary>
        /// Adds an additional image URL to the catalog item.
        /// </summary>
        /// <param name="imageUrl">The URL of the additional image to add.</param>
        public void AddAdditionalImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));

            var imageUrls = new List<string>(AdditionalImageUrls) { imageUrl };
            AdditionalImageUrls = imageUrls;
        }

        /// <summary>
        /// Removes a main image URL from the catalog item.
        /// </summary>
        /// <param name="imageUrl">The URL of the main image to remove.</param>
        public void RemoveMainImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));

            var imageUrls = new List<string>(MainImageUrls);
            imageUrls.Remove(imageUrl);
            MainImageUrls = imageUrls;
        }

        /// <summary>
        /// Removes an additional image URL from the catalog item.
        /// </summary>
        /// <param name="imageUrl">The URL of the additional image to remove.</param>
        public void RemoveAdditionalImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));

            var imageUrls = new List<string>(AdditionalImageUrls);
            imageUrls.Remove(imageUrl);
            AdditionalImageUrls = imageUrls;
        }
    }
}
