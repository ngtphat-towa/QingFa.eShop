namespace QingFa.EShop.Domain.Catalogs.ValueObjects;

/// <summary>
/// Represents a unique identifier for a size.
/// </summary>
public record SizeId
{
    /// <summary>
    /// Gets the unique identifier value for the size.
    /// </summary>
    public int Value { get; }

    // Private constructor to ensure encapsulation and immutability
    private SizeId(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SizeId"/> class.
    /// </summary>
    /// <param name="value">The unique identifier value for the size.</param>
    /// <returns>A new <see cref="SizeId"/> instance.</returns>
    public static SizeId Create(int value)
    {
        return new SizeId(value);
    }
}