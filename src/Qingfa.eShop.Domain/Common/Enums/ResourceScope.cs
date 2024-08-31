using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Common.Enums
{
    public class ResourceScope : Enumeration<ResourceScope>
    {
        public static readonly ResourceScope Category = new(1, nameof(Category));
        public static readonly ResourceScope Brand = new(2, nameof(Brand));
        public static readonly ResourceScope Product = new(3, nameof(Product));
        public static readonly ResourceScope AttibuteGroup = new(4, nameof(AttibuteGroup));
        public static readonly ResourceScope Attribute = new(5, nameof(Attribute));
        public static readonly ResourceScope AttributeOption = new(6, nameof(AttributeOption));

        private ResourceScope(int id, string name) : base(id, name) { }
    }
}
