using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities;

/// <summary>
/// Represents a color with a name, hexadecimal code, and description.
/// </summary>
public class Color : Entity<ColorId>
{
    /// <summary>
    /// Gets the name of the color.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the hexadecimal color code.
    /// </summary>
    public string HexCode { get; private set; }

    /// <summary>
    /// Gets the description of the color.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the color.</param>
    /// <param name="name">The name of the color.</param>
    /// <param name="hexCode">The hexadecimal color code.</param>
    /// <param name="description">A description of the color.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="hexCode"/> is invalid.</exception>
    private Color(ColorId id, string name, string hexCode, string description)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Color name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(hexCode)) throw new ArgumentException("Hex code is required.", nameof(hexCode));
        if (hexCode.Length != 7 || hexCode[0] != '#' || !System.Text.RegularExpressions.Regex.IsMatch(hexCode.Substring(1), @"^[0-9A-Fa-f]{6}$"))
            throw new ArgumentException("Hex code must be a valid hexadecimal color code.", nameof(hexCode));

        Name = name;
        HexCode = hexCode;
        Description = description;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Color"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the color.</param>
    /// <param name="name">The name of the color.</param>
    /// <param name="hexCode">The hexadecimal color code.</param>
    /// <param name="description">A description of the color.</param>
    /// <returns>A new <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="hexCode"/> is invalid.</exception>
    public static Color Create(ColorId id, string name, string hexCode, string description)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));

        return new Color(id, name, hexCode, description);
    }
}
