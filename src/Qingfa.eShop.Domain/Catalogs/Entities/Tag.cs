using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Tag : Entity<TagId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Tag(TagId tagId, string name, string description)
            : base(tagId)
        {
            Name = name ?? throw CoreException.NullArgument(nameof(name));
            Description = description;
        }

        public static Tag Create(TagId tagId, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw CoreException.NullArgument(nameof(name));
            return new Tag(tagId, name, description);
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
#pragma warning disable CS8618
        protected Tag()
#pragma warning restore CS8618
        {

        }
    }
}
