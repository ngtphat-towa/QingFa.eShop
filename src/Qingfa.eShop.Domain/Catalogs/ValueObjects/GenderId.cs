namespace QingFa.EShop.Domain.Catalogs.ValueObjects;

/// <summary>
/// Represents a unique identifier for a size.
/// </summary>
public record GenderId
{
    /// <summary>
    /// Gets the unique identifier value for the size.
    /// </summary>
    public int Value { get; }

    // Private constructor to ensure encapsulation and immutability
    private GenderId(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="GenderId"/> class.
    /// </summary>
    /// <param name="value">The unique identifier value for the size.</param>
    /// <returns>A new <see cref="GenderId"/> instance.</returns>
    public static GenderId Create(int value)
    {
        return new GenderId(value);
    }
}