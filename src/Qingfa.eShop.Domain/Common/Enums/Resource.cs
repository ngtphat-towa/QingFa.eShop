using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Common.Enums
{
    public class Resource : Enumeration<Resource>
    {
        public static readonly Resource Category = new(1, nameof(Category));
        public static readonly Resource Brand = new(2, nameof(Brand));
        public static readonly Resource Product = new(3, nameof(Product));
        public static readonly Resource AttibuteGroup = new(4, nameof(AttibuteGroup));
        public static readonly Resource Attribute = new(5, nameof(Attribute));
        public static readonly Resource AttributeOption = new(6, nameof(AttributeOption));

        private Resource(int id, string name) : base(id, name) { }
    }
}
