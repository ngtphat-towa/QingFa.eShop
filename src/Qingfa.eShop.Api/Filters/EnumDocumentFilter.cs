using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QingFa.EShop.Api.Filters
{
    /// <summary>
    /// A document filter that enhances Swagger documentation by adding descriptions to enum values.
    /// </summary>
    public class EnumDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies the filter to the Swagger document to add descriptions for enum values.
        /// </summary>
        /// <param name="swaggerDoc">The Swagger document to apply the filter to.</param>
        /// <param name="context">The context for the document filter.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Add enum descriptions to result models
            foreach (var schemaPair in swaggerDoc.Components.Schemas)
            {
                var schema = schemaPair.Value;
                foreach (var propertyPair in schema.Properties)
                {
                    var property = propertyPair.Value;
                    if (property.Enum != null && property.Enum.Count > 0)
                    {
                        property.Description += DescribeEnum(property.Enum);
                    }
                }
            }

            if (swaggerDoc.Paths.Count > 0)
            {
                // Add enum descriptions to input parameters
                foreach (var pathItemPair in swaggerDoc.Paths)
                {
                    var pathItem = pathItemPair.Value;
                    DescribeEnumParameters(pathItem.Parameters);

                    // Check each operation (Get, Post, Put, etc.)
                    foreach (var operationPair in pathItem.Operations)
                    {
                        DescribeEnumParameters(operationPair.Value.Parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Adds descriptions for enum values in request parameters.
        /// </summary>
        /// <param name="parameters">The collection of parameters to process.</param>
        private static void DescribeEnumParameters(IEnumerable<OpenApiParameter> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                if (param.Schema != null && param.Schema.Enum != null && param.Schema.Enum.Count > 0)
                {
                    param.Description += DescribeEnum(param.Schema.Enum);
                }
            }
        }

        /// <summary>
        /// Generates a description string for a list of enum values.
        /// </summary>
        /// <param name="enums">The list of enum values to describe.</param>
        /// <returns>A string containing the descriptions of the enum values.</returns>
        private static string DescribeEnum(IList<IOpenApiAny> enums)
        {
            var enumDescriptions = new List<string>();
            Type? type = null;

            foreach (var enumOption in enums)
            {
                if (enumOption != null)
                {
                    if (type == null)
                    {
                        // Get the underlying type of the enum
                        type = enumOption.GetType().GetGenericArguments().FirstOrDefault();
                    }
                    if (type != null)
                    {
                        var enumValue = Convert.ChangeType(enumOption, type.GetEnumUnderlyingType());
                        var enumName = Enum.GetName(type, enumValue);
                        if (enumName != null)
                        {
                            enumDescriptions.Add($"{enumValue} = {enumName}");
                        }
                    }
                }
            }

            return $"{Environment.NewLine}{string.Join(Environment.NewLine, enumDescriptions)}";
        }
    }
}
