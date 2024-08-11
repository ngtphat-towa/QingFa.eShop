using QingFa.EShop.Domain.DomainModels.Exceptions;
using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents a material used in products.
    /// </summary>
    public class Material : Entity<MaterialId>
    {
        /// <summary>
        /// Gets the name of the material.
        /// </summary>
        public string Name { get; private set; }
        public string? Description { get; private set; }

        private Material(MaterialId id, string name, string? description) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullOrEmptyArgument(nameof(name));
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Material"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the material.</param>
        /// <param name="name">The name of the material.</param>
        /// <returns>A new instance of the <see cref="Material"/> class.</returns>
        public static Material Create(MaterialId id, string name,string? description)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return new Material(id, name, description);
        }
    }
}
