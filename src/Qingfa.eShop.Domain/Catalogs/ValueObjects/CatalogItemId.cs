namespace QingFa.EShop.Domain.Catalogs.ValueObjects;

/// <summary>
/// Represents a unique identifier for a color.
/// </summary>
public record CatalogItemId
{
    /// <summary>
    /// Gets the unique identifier value for the color.
    /// </summary>
    public int Value { get; }

    // Private constructor to ensure encapsulation and immutability
    private CatalogItemId(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="CatalogItemId"/> class.
    /// </summary>
    /// <param name="value">The unique identifier value for the color.</param>
    /// <returns>A new <see cref="CatalogItemId"/> instance.</returns>
    public static CatalogItemId Create(int value)
    {
        return new CatalogItemId(value);
    }
}