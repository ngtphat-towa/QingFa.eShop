using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities;

/// <summary>
/// Represents a tag in the catalog with properties for name, description, and color.
/// </summary>
public class Tag : Entity<TagId>
{
    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    /// <value>
    /// The name of the tag (e.g., "New Arrival", "Sale").
    /// </value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the tag.
    /// </summary>
    /// <value>
    /// The description of the tag (optional).
    /// </value>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the hexadecimal color code associated with the tag.
    /// </summary>
    /// <value>
    /// The hexadecimal color code for the tag display (optional).
    /// </value>
    public string ColorCode { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the tag.</param>
    /// <param name="name">The name of the tag.</param>
    /// <param name="description">The description of the tag.</param>
    /// <param name="colorCode">The hexadecimal color code for the tag.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or empty.</exception>
    private Tag(TagId id, string name, string description, string colorCode)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        ColorCode = colorCode;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the tag.</param>
    /// <param name="name">The name of the tag.</param>
    /// <param name="description">The description of the tag.</param>
    /// <param name="colorCode">The hexadecimal color code for the tag.</param>
    /// <returns>A new <see cref="Tag"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null or empty.</exception>
    public static Tag Create(TagId id, string name, string description, string colorCode)
    {
        return new Tag(id, name, description, colorCode);
    }
}
