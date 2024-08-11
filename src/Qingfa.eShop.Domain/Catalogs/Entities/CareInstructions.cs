using QingFa.EShop.Domain.Catalogs.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    /// <summary>
    /// Represents care instructions for a product within the e-shop domain.
    /// </summary>
    public class CareInstructions : Entity<CareInstructionId>
    {
        /// <summary>
        /// Gets the name of the care instructions.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the care instructions.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CareInstructions"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the care instructions.</param>
        /// <param name="name">The name of the care instructions.</param>
        /// <param name="description">A description of the care instructions.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        private CareInstructions(CareInstructionId id, string name, string description) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("CareInstructions name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("CareInstructions description cannot be empty.", nameof(description));

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CareInstructions"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the care instructions.</param>
        /// <param name="name">The name of the care instructions.</param>
        /// <param name="description">A description of the care instructions.</param>
        /// <returns>A new <see cref="CareInstructions"/> instance.</returns>
        public static CareInstructions Create(CareInstructionId id, string name, string description)
        {
            return new CareInstructions(id, name, description);
        }

        /// <summary>
        /// Updates the name and description of the care instructions.
        /// </summary>
        /// <param name="name">The new name of the care instructions.</param>
        /// <param name="description">The new description of the care instructions.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="description"/> is null or whitespace.</exception>
        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("CareInstructions name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("CareInstructions description cannot be empty.", nameof(description));

            Name = name;
            Description = description;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
