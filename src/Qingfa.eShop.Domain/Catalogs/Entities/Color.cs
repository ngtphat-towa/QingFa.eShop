using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
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
        /// <exception cref="ArgumentInvalidException">Thrown when <paramref name="name"/> or <paramref name="hexCode"/> is invalid.</exception>
        public Color(ColorId id, string name, string hexCode, string description)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(hexCode)) throw CoreException.NullOrEmptyArgument(nameof(hexCode));
            if (hexCode.Length != 7 || hexCode[0] != '#' || !System.Text.RegularExpressions.Regex.IsMatch(hexCode.Substring(1), @"^[0-9A-Fa-f]{6}$"))
                throw CoreException.InvalidArgument(nameof(hexCode));

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
        /// <exception cref="ArgumentInvalidException">Thrown when <paramref name="name"/> or <paramref name="hexCode"/> is invalid.</exception>
        public static Color Create(ColorId id, string name, string hexCode, string description)
        {
            if (id == null) throw CoreException.NullArgument(nameof(id));

            return new Color(id, name, hexCode, description);
        }
    }
}
