using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels.Exceptions;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents an age group within the e-shop domain.
    /// </summary>
    public class AgeGroup : Entity<AgeGroupId>
    {
        /// <summary>
        /// Gets the name of the age group.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the age group.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgeGroup"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the age group.</param>
        /// <param name="name">The name of the age group.</param>
        /// <param name="description">A description of the age group.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        private AgeGroup(AgeGroupId id, string name, string description) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AgeGroup"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the age group.</param>
        /// <param name="name">The name of the age group.</param>
        /// <param name="description">A description of the age group.</param>
        /// <returns>A new <see cref="AgeGroup"/> instance.</returns>
        public static AgeGroup Create(AgeGroupId id, string name, string description)
        {
            return new AgeGroup(id, name, description);
        }

        /// <summary>
        /// Updates the name and description of the age group.
        /// </summary>
        /// <param name="name">The new name of the age group.</param>
        /// <param name="description">The new description of the age group.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw CoreException.NullOrEmptyArgument(nameof(description));

            Name = name;
            Description = description;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
