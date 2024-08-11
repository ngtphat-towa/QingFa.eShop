using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities;

/// <summary>
/// Represents a size within the e-shop domain.
/// </summary>
public class Size : Entity<SizeId>
{
    /// <summary>
    /// Gets the name of the size.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets a description or measurements related to the size.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the URL to the size chart.
    /// </summary>
    public string SizeChartURL { get; private set; }

    /// <summary>
    /// Gets the size type (e.g., "US", "EU").
    /// </summary>
    public string SizeType { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Size"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the size.</param>
    /// <param name="name">The name of the size.</param>
    /// <param name="description">A description or measurements related to the size.</param>
    /// <param name="sizeChartURL">The URL to the size chart.</param>
    /// <param name="sizeType">The size type (e.g., "US").</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="sizeType"/> is null or empty.</exception>
    private Size(SizeId id, string name, string description, string sizeChartURL, string sizeType)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Size name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(sizeType)) throw new ArgumentException("Size type is required.", nameof(sizeType));

        Name = name;
        Description = description;
        SizeChartURL = sizeChartURL;
        SizeType = sizeType;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Size"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the size.</param>
    /// <param name="name">The name of the size.</param>
    /// <param name="description">A description or measurements related to the size.</param>
    /// <param name="sizeChartURL">The URL to the size chart.</param>
    /// <param name="sizeType">The size type (e.g., "US").</param>
    /// <returns>A new <see cref="Size"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="sizeType"/> is null or empty.</exception>
    public static Size Create(SizeId id, string name, string description, string sizeChartURL, string sizeType)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Size name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(sizeType)) throw new ArgumentException("Size type is required.", nameof(sizeType));

        return new Size(id, name, description, sizeChartURL, sizeType);
    }
}