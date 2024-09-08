using MediatR;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QingFa.EShop.Application.Features.AttributeManagements.CreateAttribute
{
    public class CreateProductAttributeCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = default!;
        public string AttributeCode { get; set; } = default!;
        public string? Description { get; set; }
        public ProductAttribute.AttributeType Type { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFilterable { get; set; }
        public bool ShowToCustomers { get; set; }
        public int SortOrder { get; set; }
        public Guid AttributeGroupId { get; set; }
    }

    internal class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommand, Result<Guid>>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<CreateProductAttributeCommandHandler> _logger;

        public CreateProductAttributeCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<CreateProductAttributeCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            // Validate request parameters
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<Guid>.InvalidArgument(nameof(request.Name), "Name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.AttributeCode))
                return Result<Guid>.InvalidArgument(nameof(request.AttributeCode), "Attribute code cannot be null or empty.");

            _logger.LogInformation("Handling CreateProductAttributeCommand with Name: {Name}, AttributeCode: {AttributeCode}, AttributeGroupId: {AttributeGroupId}",
                                    request.Name,
                                    request.AttributeCode,
                                    request.AttributeGroupId);

            // Check if the attribute group exists
            var groupExists = await _dbProvider.ProductAttributeGroups
                .AnyAsync(g => g.Id == request.AttributeGroupId, cancellationToken);
            if (!groupExists)
            {
                return Result<Guid>.NotFound(nameof(ProductAttributeGroup), "Attribute group not found.");
            }

            // Check if an attribute with the same name exists in the specified attribute group
            var attributeExists = await _dbProvider.ProductAttributes
                .AnyAsync(a => a.Name == request.Name && a.AttributeGroupId == request.AttributeGroupId, cancellationToken);
            if (attributeExists)
            {
                return Result<Guid>.Conflict(nameof(ProductAttribute), "An attribute with this name already exists in the specified attribute group.");
            }

            try
            {
                // Create the new product attribute
                var attribute = ProductAttribute.Create(
                    request.Name,
                    request.AttributeCode,
                    request.Description,
                    request.Type,
                    request.IsRequired,
                    request.IsFilterable,
                    request.ShowToCustomers,
                    request.SortOrder,
                    request.AttributeGroupId);

                await _dbProvider.ProductAttributes.AddAsync(attribute, cancellationToken);
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Created new product attribute with ID: {AttributeId}", attribute.Id);

                return Result<Guid>.Success(attribute.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product attribute.");
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
