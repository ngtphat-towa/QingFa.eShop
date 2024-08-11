using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a type or category of clothing in the catalog.
    /// </summary>
    public class CatalogType : Entity<CatalogTypeId>
    {
        /// <summary>
        /// Gets the name of the clothing type.
        /// </summary>
        /// <remarks>
        /// This represents the category of clothing, such as "T-Shirt", "Jacket", "Jeans", etc.
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the clothing type.
        /// </summary>
        /// <remarks>
        /// Provides additional details about the clothing type, describing its features or intended use.
        /// </remarks>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogType"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the clothing type.</param>
        /// <param name="name">The name of the clothing type.</param>
        /// <param name="description">The description of the clothing type.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        public CatalogType(CatalogTypeId id, string name, string description)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Updates the name of the clothing type.
        /// </summary>
        /// <param name="name">The new name of the clothing type.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            Name = name;
        }

        /// <summary>
        /// Updates the description of the clothing type.
        /// </summary>
        /// <param name="description">The new description of the clothing type.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="description"/> is null or whitespace.</exception>
        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));
            Description = description;
        }
    }
}
