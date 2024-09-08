using MediatR;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QingFa.EShop.Application.Features.AttributeManagements.UpdateAttribute
{
    public record UpdateProductAttributeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public string AttributeCode { get; init; } = string.Empty;
        public string? Description { get; init; }
        public ProductAttribute.AttributeType Type { get; init; }
        public bool IsRequired { get; init; }
        public bool IsFilterable { get; init; }
        public bool ShowToCustomers { get; init; }
        public int SortOrder { get; init; }
        public Guid AttributeGroupId { get; init; }
    }

    internal class UpdateProductAttributeCommandHandler : IRequestHandler<UpdateProductAttributeCommand, Result<Guid>>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<UpdateProductAttributeCommandHandler> _logger;

        public UpdateProductAttributeCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<UpdateProductAttributeCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request parameters
                if (string.IsNullOrWhiteSpace(request.Name))
                    return Result<Guid>.InvalidArgument(nameof(request.Name), "Name cannot be null or empty.");

                if (string.IsNullOrWhiteSpace(request.AttributeCode))
                    return Result<Guid>.InvalidArgument(nameof(request.AttributeCode), "Attribute code cannot be null or empty.");

                // Check if the attribute exists
                var existingAttribute = await _dbProvider.ProductAttributes
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (existingAttribute == null)
                    return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.Id}");

                // Check if the attribute group exists
                var attributeGroupExists = await _dbProvider.ProductAttributeGroups
                    .FindAsync(new object[] { request.AttributeGroupId }, cancellationToken);

                if (attributeGroupExists == null)
                    return Result<Guid>.NotFound(nameof(ProductAttributeGroup), $"No attribute group found with ID: {request.AttributeGroupId}");

                // Update the product attribute
                existingAttribute.Update(
                    request.Name,
                    request.AttributeCode,
                    request.Description,
                    request.Type,
                    request.IsRequired,
                    request.IsFilterable,
                    request.ShowToCustomers,
                    request.SortOrder);

                // If the attribute group ID has changed, ensure it's valid
                if (existingAttribute.AttributeGroupId != request.AttributeGroupId)
                {
                    existingAttribute.UpdateAttributeGroup(request.AttributeGroupId);
                }

                // Save changes
                await _dbProvider.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(existingAttribute.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product attribute.");
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
