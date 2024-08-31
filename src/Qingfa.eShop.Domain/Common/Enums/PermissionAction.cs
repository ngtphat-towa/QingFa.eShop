using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Common.Enums
{
    public class PermissionAction : Enumeration<PermissionAction>
    {
        public static readonly PermissionAction Create = new(1, "Create");
        public static readonly PermissionAction Update = new(2, "Update");
        public static readonly PermissionAction Delete = new(3, "Delete");
        public static readonly PermissionAction Read = new(4, "Read");
        public static readonly PermissionAction Import = new(5, "Import");

        private PermissionAction(int id, string name) : base(id, name) { }
    }
}
