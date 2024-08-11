using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a gender category in the catalog.
    /// </summary>
    public class Gender : Entity<GenderId>
    {
        /// <summary>
        /// Gets the name of the gender category.
        /// </summary>
        /// <remarks>
        /// This represents the gender category, such as "Men", "Women", "Unisex", etc.
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the gender category.
        /// </summary>
        /// <remarks>
        /// Provides additional details about the gender category, describing its target audience or usage.
        /// </remarks>
        public string? Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Gender"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the gender category.</param>
        /// <param name="name">The name of the gender category.</param>
        /// <param name="description">The description of the gender category.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
        private Gender(GenderId id, string name, string? description)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new <see cref="Gender"/> instance.
        /// </summary>
        /// <param name="id">The unique identifier of the gender category.</param>
        /// <param name="name">The name of the gender category. Defaults to "None".</param>
        /// <param name="description">The description of the gender category. Defaults to null.</param>
        /// <returns>A new <see cref="Gender"/> instance.</returns>
        public static Gender Create(GenderId id, string name = "None", string? description = null)
        {
            return new Gender(id, name, description);
        }

        /// <summary>
        /// Updates the name of the gender category.
        /// </summary>
        /// <param name="name">The new name of the gender category.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            Name = name;
            UpdatedTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the description of the gender category.
        /// </summary>
        /// <param name="description">The new description of the gender category.</param>
        public void UpdateDescription(string? description)
        {
            Description = description;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
