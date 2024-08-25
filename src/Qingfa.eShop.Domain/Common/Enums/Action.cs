using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Common.Enums
{
    public class Action : Enumeration<Action>
    {
        public static readonly Action Create = new(1, "Create");
        public static readonly Action Update = new(2, "Update");
        public static readonly Action Delete = new(3, "Delete");
        public static readonly Action Read = new(4, "Read");
        public static readonly Action Import = new(5, "Import");

        private Action(int id, string name) : base(id, name) { }
    }
}
