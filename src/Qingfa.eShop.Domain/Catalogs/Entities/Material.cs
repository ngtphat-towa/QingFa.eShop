using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Material : Entity<MaterialId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MaterialCategory Category { get; private set; }
        public string Properties { get; private set; } // e.g., "Water-resistant, breathable"

        private Material(MaterialId materialId, string name, string description, MaterialCategory category, string properties)
            : base(materialId)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description;
            Category = category ?? throw CoreException.NullArgument(nameof(category));
            Properties = properties;
        }

        public static Material Create(MaterialId materialId, string name, string description, MaterialCategory category, string properties)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullArgument(nameof(name));
            if (category == null) throw CoreException.NullArgument(nameof(category));
            return new Material(materialId, name, description, category, properties);
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateCategory(MaterialCategory category)
        {
            if (category == null) throw CoreException.NullArgument(nameof(category));
            Category = category;
        }

        public void UpdateProperties(string properties)
        {
            Properties = properties;
        }
#pragma warning disable CS8618
        protected Material()
#pragma warning restore CS8618
        {

        }
    }

    public class MaterialCategory : ValueObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public MaterialCategory(string name, string description)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
