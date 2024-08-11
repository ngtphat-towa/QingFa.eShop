using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a size within the e-shop domain.
    /// </summary>
    public class Size : Entity<SizeId>
    {
        // Standard size names
        public static readonly string XS = "XS";
        public static readonly string S = "S";
        public static readonly string M = "M";
        public static readonly string L = "L";
        public static readonly string XL = "XL";
        public static readonly string XXL = "XXL";
        // Add more sizes as needed

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
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="sizeType"/> is null or empty, or when <paramref name="name"/> is not a valid size name.</exception>
        private Size(SizeId id, string name, string description, string sizeChartURL, string sizeType)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name) || !IsValidSizeName(name))
                throw CoreException.InvalidArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(sizeType))
                throw CoreException.NullOrEmptyArgument(nameof(sizeType));

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
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="sizeType"/> is null or empty, or when <paramref name="name"/> is not a valid size name.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null.</exception>
        public static Size Create(SizeId id, string name, string description, string sizeChartURL, string sizeType)
        {
            if (id == null) throw CoreException.NullOrEmptyArgument(nameof(id));
            if (string.IsNullOrWhiteSpace(name) || !IsValidSizeName(name))
                throw CoreException.InvalidArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(sizeType))
                throw CoreException.NullOrEmptyArgument(nameof(sizeType));

            return new Size(id, name, description, sizeChartURL, sizeType);
        }

        /// <summary>
        /// Checks if the provided size name is valid.
        /// </summary>
        /// <param name="sizeName">The size name to validate.</param>
        /// <returns>True if the size name is valid; otherwise, false.</returns>
        private static bool IsValidSizeName(string sizeName)
        {
            return sizeName == XS || sizeName == S || sizeName == M || sizeName == L || sizeName == XL || sizeName == XXL;
        }

        /// <summary>
        /// Updates the name of the size.
        /// </summary>
        /// <param name="name">The new name of the size.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null, empty, or not a valid size name.</exception>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !IsValidSizeName(name))
                throw CoreException.InvalidArgument(nameof(name));
            Name = name;
        }

        /// <summary>
        /// Updates the description of the size.
        /// </summary>
        /// <param name="description">The new description of the size.</param>
        public void UpdateDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Updates the URL to the size chart.
        /// </summary>
        /// <param name="sizeChartURL">The new URL to the size chart.</param>
        public void UpdateSizeChartURL(string sizeChartURL)
        {
            SizeChartURL = sizeChartURL;
        }

        /// <summary>
        /// Updates the size type.
        /// </summary>
        /// <param name="sizeType">The new size type (e.g., "US").</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="sizeType"/> is null or empty.</exception>
        public void UpdateSizeType(string sizeType)
        {
            if (string.IsNullOrWhiteSpace(sizeType)) throw CoreException.NullOrEmptyArgument(nameof(sizeType));
            SizeType = sizeType;
        }
    }
}
