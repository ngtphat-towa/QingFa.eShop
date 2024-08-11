using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Categories.ValueObjects;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Catalogs
{
    /// <summary>
    /// Represents an item in the catalog.
    /// </summary>
    public class CatalogItem : AggregateBase<CatalogItemId>
    {
        private readonly List<Size> _sizeOptions = new();
        private readonly List<Color> _colorOptions = new();
        private readonly List<Tag> _tags = new();

        /// <summary>
        /// Gets the unique identifier for the catalog item.
        /// </summary>
        public CatalogItemId CatalogItemId { get; }

        /// <summary>
        /// Gets the name of the catalog item.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the short description of the catalog item.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the detailed description of the catalog item.
        /// </summary>
        public string LongDescription { get; }

        /// <summary>
        /// Gets the unique identifier for the category.
        /// </summary>
        public CategoryId? CategoryId { get; }

        /// <summary>
        /// Gets the unique identifier for the subcategory.
        /// </summary>
        public SubCategoryId? SubCategoryId { get; }

        /// <summary>
        /// Gets the unique identifier for the brand.
        /// </summary>
        public CatalogBrandId? CatalogBrandId { get; }

        /// <summary>
        /// Gets the size options available for the catalog item.
        /// </summary>
        public IReadOnlyCollection<Size> SizeOptions => _sizeOptions.AsReadOnly();

        /// <summary>
        /// Gets the color options available for the catalog item.
        /// </summary>
        public IReadOnlyCollection<Color> ColorOptions => _colorOptions.AsReadOnly();

        /// <summary>
        /// Gets the material composition of the catalog item.
        /// </summary>
        public string Material { get; }

        /// <summary>
        /// Gets the price of the catalog item.
        /// </summary>
        public Price Price { get; }

        /// <summary>
        /// Gets the discount price of the catalog item.
        /// </summary>
        public Price? DiscountPrice { get; }

        /// <summary>
        /// Gets the available stock quantity of the catalog item.
        /// </summary>
        public int StockQuantity { get; }

        /// <summary>
        /// Gets the stock keeping unit of the catalog item.
        /// </summary>
        public string SKU { get; }

        /// <summary>
        /// Gets the URL for the main image of the catalog item.
        /// </summary>
        public string MainImageURL { get; }

        /// <summary>
        /// Gets the average customer rating for the catalog item.
        /// </summary>
        public decimal Rating { get; }

        /// <summary>
        /// Gets the number of reviews for the catalog item.
        /// </summary>
        public int ReviewCount { get; }

        /// <summary>
        /// Gets the tags associated with the catalog item.
        /// </summary>
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        /// <summary>
        /// Gets the seasonal collection for the catalog item.
        /// </summary>
        public string Season { get; }

        /// <summary>
        /// Gets the intended gender for the catalog item.
        /// </summary>
        public string Gender { get; }

        /// <summary>
        /// Gets the age group for the catalog item.
        /// </summary>
        public string AgeGroup { get; }

        /// <summary>
        /// Gets the care instructions for the catalog item.
        /// </summary>
        public string CareInstructions { get; }

        /// <summary>
        /// Gets a value indicating whether the catalog item is active.
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="CatalogItem"/> class.
        /// </summary>
        /// <param name="catalogItemId">The unique identifier for the catalog item.</param>
        /// <param name="name">The name of the catalog item.</param>
        /// <param name="description">The short description of the catalog item.</param>
        /// <param name="longDescription">The detailed description of the catalog item.</param>
        /// <param name="categoryId">The unique identifier for the category.</param>
        /// <param name="subCategoryId">The unique identifier for the subcategory.</param>
        /// <param name="catalogBrandId">The unique identifier for the brand.</param>
        /// <param name="sizeOptions">The size options available for the catalog item.</param>
        /// <param name="colorOptions">The color options available for the catalog item.</param>
        /// <param name="material">The material composition of the catalog item.</param>
        /// <param name="price">The price of the catalog item.</param>
        /// <param name="discountPrice">The discount price of the catalog item.</param>
        /// <param name="stockQuantity">The available stock quantity of the catalog item.</param>
        /// <param name="sku">The stock keeping unit of the catalog item.</param>
        /// <param name="mainImageURL">The URL for the main image of the catalog item.</param>
        /// <param name="rating">The average customer rating for the catalog item.</param>
        /// <param name="reviewCount">The number of reviews for the catalog item.</param>
        /// <param name="tags">The tags associated with the catalog item.</param>
        /// <param name="season">The seasonal collection for the catalog item.</param>
        /// <param name="gender">The intended gender for the catalog item.</param>
        /// <param name="ageGroup">The age group for the catalog item.</param>
        /// <param name="careInstructions">The care instructions for the catalog item.</param>
        /// <param name="creationDate">The date when the catalog item was created.</param>
        /// <param name="updateDate">The date when the catalog item was last updated.</param>
        /// <param name="active">A value indicating whether the catalog item is active.</param>
        /// <returns>A new instance of <see cref="CatalogItem"/>.</returns>
        public static CatalogItem Create(
            CatalogItemId catalogItemId,
            string name,
            string description,
            string longDescription,
            CategoryId? categoryId,
            SubCategoryId? subCategoryId,
            CatalogBrandId? catalogBrandId,
            IEnumerable<Size> sizeOptions,
            IEnumerable<Color> colorOptions,
            string material,
            Price price,
            Price? discountPrice,
            int stockQuantity,
            string sku,
            string mainImageURL,
            decimal rating,
            int reviewCount,
            IEnumerable<Tag> tags,
            string season,
            string gender,
            string ageGroup,
            string careInstructions,
            DateTime creationDate,
            DateTime updateDate,
            bool active)
        {
            ValidateInput(name, description, longDescription, material, price, sku, mainImageURL, rating, reviewCount, season, gender, ageGroup, careInstructions);

            var catalogItem = new CatalogItem(
                catalogItemId,
                name,
                description,
                longDescription,
                categoryId,
                subCategoryId,
                catalogBrandId,
                sizeOptions.ToList(),
                colorOptions.ToList(),
                material,
                price,
                discountPrice,
                stockQuantity,
                sku,
                mainImageURL,
                rating,
                reviewCount,
                tags.ToList(),
                season,
                gender,
                ageGroup,
                careInstructions,
                creationDate,
                updateDate,
                active);

            return catalogItem;
        }

        private CatalogItem(
            CatalogItemId catalogItemId,
            string name,
            string description,
            string longDescription,
            CategoryId? categoryId,
            SubCategoryId? subCategoryId,
            CatalogBrandId? catalogBrandId,
            List<Size> sizeOptions,
            List<Color> colorOptions,
            string material,
            Price price,
            Price? discountPrice,
            int stockQuantity,
            string sku,
            string mainImageURL,
            decimal rating,
            int reviewCount,
            List<Tag> tags,
            string season,
            string gender,
            string ageGroup,
            string careInstructions,
            DateTime creationDate,
            DateTime updateDate,
            bool active)
        {
            CatalogItemId = catalogItemId;
            Name = name;
            Description = description;
            LongDescription = longDescription;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            CatalogBrandId = catalogBrandId;
            _sizeOptions = sizeOptions;
            _colorOptions = colorOptions;
            Material = material;
            Price = price;
            DiscountPrice = discountPrice;
            StockQuantity = stockQuantity;
            SKU = sku;
            MainImageURL = mainImageURL;
            Rating = rating;
            ReviewCount = reviewCount;
            _tags = tags;
            Season = season;
            Gender = gender;
            AgeGroup = ageGroup;
            CareInstructions = careInstructions;
            UpdatedTime = updateDate;
            Active = active;
        }

        private static void ValidateInput(string name, string description, string longDescription, string material, Price price, string sku, string mainImageURL, decimal rating, int reviewCount, string season, string gender, string ageGroup, string careInstructions)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(longDescription)) throw new ArgumentException("LongDescription cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(material)) throw new ArgumentException("Material cannot be null or empty.");
            if (price == null) throw new ArgumentNullException(nameof(price));
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(mainImageURL)) throw new ArgumentException("MainImageURL cannot be null or empty.");
            if (rating < 0 || rating > 5) throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5.");
            if (reviewCount < 0) throw new ArgumentOutOfRangeException(nameof(reviewCount), "Review count cannot be negative.");
            if (string.IsNullOrWhiteSpace(season)) throw new ArgumentException("Season cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(gender)) throw new ArgumentException("Gender cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(ageGroup)) throw new ArgumentException("AgeGroup cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(careInstructions)) throw new ArgumentException("CareInstructions cannot be null or empty.");
        }
    }
}
