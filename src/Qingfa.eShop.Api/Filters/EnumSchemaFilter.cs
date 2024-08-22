using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace QingFa.EShop.Api.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                ApplyEnumSchema(schema, context.Type);
            }
            else if (IsListOfEnum(context.Type, out var enumType))
            {
                // Ensure enumType is not null before calling ApplyEnumListSchema
                if (enumType != null)
                {
                    ApplyEnumListSchema(schema, enumType);
                }
            }
        }

        private static void ApplyEnumSchema(OpenApiSchema schema, Type enumType)
        {
            var enumValues = Enum.GetValues(enumType)
                .Cast<Enum>()
                .ToArray();

            var enumDescriptions = enumValues.ToDictionary(
                value => Convert.ToInt32(value),
                GetEnumDescription
            );

            schema.Description = BuildEnumDescription(enumType, enumDescriptions);
            schema.Example = enumValues.Length > 0
                ? new OpenApiInteger(Convert.ToInt32(enumValues.First()))
                : null;
        }

        private static void ApplyEnumListSchema(OpenApiSchema schema, Type enumType)
        {
            var enumValues = Enum.GetValues(enumType)
                .Cast<Enum>()
                .ToArray();

            schema.Items = new OpenApiSchema
            {
                Type = "integer",
                Format = "int32"
            };

            schema.Enum = enumValues
                .Select(value => (IOpenApiAny)new OpenApiInteger(Convert.ToInt32(value)))
                .ToList();

            schema.Description = BuildEnumDescription(enumType, enumValues.ToDictionary(
                value => Convert.ToInt32(value),
                GetEnumDescription
            ));
        }

        private static bool IsListOfEnum(Type type, out Type? enumType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var argument = type.GetGenericArguments().FirstOrDefault();
                if (argument?.IsEnum == true)
                {
                    enumType = argument;
                    return true;
                }
            }

            enumType = null;
            return false;
        }

        private static string BuildEnumDescription(Type enumType, Dictionary<int, string> enumDescriptions)
        {
            var descriptions = new StringBuilder();
            descriptions.AppendLine($"{enumType.Name} values:<br>");
            foreach (var (intValue, description) in enumDescriptions)
            {
                descriptions.AppendLine($"{intValue} - {description}<br>");
            }
            return descriptions.ToString();
        }

        private static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? value.ToString();
        }
    }
}
