using QingFa.EShop.Domain.Catalogs.ValueObjects;

using QingFa.EShop.Domain.DomainModels.Exceptions;

/// <summary>
/// Represents a sub-category within a catalog category.
/// </summary>
public class CatalogSubCategory : Entity<CatalogSubCategoryId>
{
    /// <summary>
    /// Gets the name of the sub-category.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the sub-category.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the identifier of the parent category.
    /// </summary>
    public CatalogCategoryId ParentCategoryId { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogSubCategory"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the sub-category.</param>
    /// <param name="name">The name of the sub-category.</param>
    /// <param name="description">The description of the sub-category.</param>
    /// <param name="parentCategoryId">The identifier of the parent category.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="parentCategoryId"/> is null.</exception>
    public CatalogSubCategory(CatalogSubCategoryId id, string name, string description, CatalogCategoryId parentCategoryId)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
        if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));
        if (parentCategoryId == null) throw new ArgumentException("Parent category ID cannot be null.", nameof(parentCategoryId));

        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }

    /// <summary>
    /// Updates the name of the sub-category.
    /// </summary>
    /// <param name="name">The new name of the sub-category.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
        Name = name;
    }

    /// <summary>
    /// Updates the description of the sub-category.
    /// </summary>
    /// <param name="description">The new description of the sub-category.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="description"/> is null or whitespace.</exception>
    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));
        Description = description;
    }
}