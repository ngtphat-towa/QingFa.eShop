using QingFa.EShop.Domain.Categories.Entities;
using QingFa.EShop.Domain.Categories.ValueObjects;

namespace QingFa.EShop.Domain.Categories;

/// <summary>
/// Represents a category within the e-shop domain.
/// </summary>
public class Category : Entity<CategoryId>
{
    private readonly List<SubCategory> _subCategories = new List<SubCategory>();

    private Category(CategoryId id, string name, string description, string imageURL, int displayOrder)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        ImageURL = imageURL;
        DisplayOrder = displayOrder;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Category"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the category.</param>
    /// <param name="name">The name of the category.</param>
    /// <param name="description">The description of the category.</param>
    /// <param name="imageURL">The URL of the image associated with the category.</param>
    /// <param name="displayOrder">The order in which the category is displayed.</param>
    /// <returns>A new instance of the <see cref="Category"/> class.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    public static Category Create(CategoryId id, string name, string description, string imageURL, int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        return new Category(id, name, description, imageURL, displayOrder);
    }

    /// <summary>
    /// Gets the name of the category.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the category.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the URL of the image associated with the category.
    /// </summary>
    public string ImageURL { get; private set; }

    /// <summary>
    /// Gets the order in which the category is displayed.
    /// </summary>
    public int DisplayOrder { get; private set; }

    /// <summary>
    /// Gets a read-only collection of subcategories within this category.
    /// </summary>
    public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();

    /// <summary>
    /// Adds a subcategory to the category.
    /// </summary>
    /// <param name="subCategory">The subcategory to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="subCategory"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="subCategory"/> does not belong to this category.</exception>
    public void AddSubCategory(SubCategory subCategory)
    {
        if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));
        if (subCategory.CategoryID != Id) throw new ArgumentException("SubCategory must belong to this Category", nameof(subCategory));

        if (_subCategories.Any(sc => sc.Id == subCategory.Id))
            throw new InvalidOperationException("SubCategory with the same ID already exists.");

        _subCategories.Add(subCategory);
    }

    /// <summary>
    /// Removes a subcategory from the category.
    /// </summary>
    /// <param name="subCategory">The subcategory to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="subCategory"/> is null.</exception>
    public void RemoveSubCategory(SubCategory subCategory)
    {
        if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));
        _subCategories.Remove(subCategory);
    }

    /// <summary>
    /// Updates the name of the category.
    /// </summary>
    /// <param name="name">The new name of the category.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        Name = name;
    }

    /// <summary>
    /// Updates the description of the category.
    /// </summary>
    /// <param name="description">The new description of the category.</param>
    public void UpdateDescription(string description)
    {
        Description = description;
    }

    /// <summary>
    /// Updates the URL of the image associated with the category.
    /// </summary>
    /// <param name="imageURL">The new URL of the image.</param>
    public void UpdateImageURL(string imageURL)
    {
        ImageURL = imageURL;
    }

    /// <summary>
    /// Updates the display order of the category.
    /// </summary>
    /// <param name="displayOrder">The new display order.</param>
    public void UpdateDisplayOrder(int displayOrder)
    {
        DisplayOrder = displayOrder;
    }

    /// <summary>
    /// Finds a subcategory by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the subcategory.</param>
    /// <returns>The subcategory with the specified identifier, or null if not found.</returns>
    public SubCategory? FindSubCategoryById(SubCategoryId id)
    {
        return _subCategories.FirstOrDefault(sc => sc.Id == id);
    }

    /// <summary>
    /// Finds a subcategory by its name.
    /// </summary>
    /// <param name="name">The name of the subcategory.</param>
    /// <returns>The subcategory with the specified name, or null if not found.</returns>
    public SubCategory? FindSubCategoryByName(string name)
    {
        return _subCategories.FirstOrDefault(sc => sc.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Clears all subcategories from the category.
    /// </summary>
    public void ClearSubCategories()
    {
        _subCategories.Clear();
    }

    /// <summary>
    /// Gets the number of subcategories in the category.
    /// </summary>
    /// <returns>The number of subcategories.</returns>
    public int GetSubCategoryCount()
    {
        return _subCategories.Count;
    }
}
