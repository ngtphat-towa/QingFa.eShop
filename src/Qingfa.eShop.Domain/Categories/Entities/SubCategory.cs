using QingFa.EShop.Domain.Categories.ValueObjects;

namespace QingFa.EShop.Domain.Categories.Entities;

/// <summary>
/// Represents a subcategory within a category in the e-shop domain.
/// </summary>
public class SubCategory : Entity<SubCategoryId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubCategory"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the subcategory.</param>
    /// <param name="categoryId">The unique identifier for the category to which this subcategory belongs.</param>
    /// <param name="name">The name of the subcategory.</param>
    /// <param name="description">The description of the subcategory.</param>
    /// <param name="imageURL">The URL of the image associated with the subcategory.</param>
    /// <param name="displayOrder">The order in which the subcategory is displayed.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="categoryId"/> or <paramref name="name"/> is null.</exception>
    private SubCategory(SubCategoryId id, CategoryId categoryId, string name, string description, string imageURL, int displayOrder)
        : base(id)
    {
        CategoryID = categoryId ?? throw new ArgumentNullException(nameof(categoryId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        ImageURL = imageURL;
        DisplayOrder = displayOrder;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SubCategory"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the subcategory.</param>
    /// <param name="categoryId">The unique identifier for the category to which this subcategory belongs.</param>
    /// <param name="name">The name of the subcategory.</param>
    /// <param name="description">The description of the subcategory.</param>
    /// <param name="imageURL">The URL of the image associated with the subcategory.</param>
    /// <param name="displayOrder">The order in which the subcategory is displayed.</param>
    /// <returns>A new instance of the <see cref="SubCategory"/> class.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="categoryId"/> or <paramref name="name"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is an empty or whitespace string.</exception>
    public static SubCategory Create(SubCategoryId id, CategoryId categoryId, string name, string description, string imageURL, int displayOrder)
    {
        if (categoryId == null) throw new ArgumentNullException(nameof(categoryId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));

        return new SubCategory(id, categoryId, name, description, imageURL, displayOrder);
    }

    /// <summary>
    /// Gets the unique identifier for the category to which this subcategory belongs.
    /// </summary>
    public CategoryId CategoryID { get; private set; }

    /// <summary>
    /// Gets the name of the subcategory.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the subcategory.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the URL of the image associated with the subcategory.
    /// </summary>
    public string ImageURL { get; private set; }

    /// <summary>
    /// Gets the order in which the subcategory is displayed.
    /// </summary>
    public int DisplayOrder { get; private set; }
}
