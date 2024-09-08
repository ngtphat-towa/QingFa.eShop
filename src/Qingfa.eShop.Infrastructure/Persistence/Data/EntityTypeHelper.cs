using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QingFa.EShop.Infrastructure.Persistence.Data
{
    internal static class EntityTypeHelper
    {
        public static IEnumerable<Type> GetDerivedEntityTypes(IModel model, Type baseType)
        {
            return model.GetEntityTypes()
                .Where(e => baseType.IsAssignableFrom(e.ClrType))
                .Select(e => e.ClrType)
                .ToList();
        }

        public static string CreateIndexName(string tableName, string columnName)
        {
            return $"IX_{tableName}_{columnName}";
        }

        public static string? GetTableName(IEntityType entityType)
        {
            return entityType.GetTableName();
        }
    }
}
