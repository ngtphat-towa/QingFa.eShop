using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a tag that can be associated with a catalog item.
    /// </summary>
    public class Tag : Entity<TagId>
    {
        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the tag.</param>
        /// <param name="name">The name of the tag.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or <paramref name="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is empty or whitespace.</exception>
        private Tag(TagId id, string name)
            : base(id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Tag name cannot be null, empty, or whitespace.", nameof(name));

            Name = name;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the tag.</param>
        /// <param name="name">The name of the tag.</param>
        /// <returns>A new <see cref="Tag"/> instance.</returns>
        public static Tag Create(TagId id, string name)
        {
            return new Tag(id, name);
        }
    }
}
