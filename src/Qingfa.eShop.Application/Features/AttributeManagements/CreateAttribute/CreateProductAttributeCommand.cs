using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

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

    internal class CreateProductAttributeCommandHandler(
        IProductAttributeRepository attributeRepository,
        IProductAttributeGroupRepository attributeGroupRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateProductAttributeCommand, Result<Guid>>
    {
        private readonly IProductAttributeRepository _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        private readonly IProductAttributeGroupRepository _attributeGroupRepository = attributeGroupRepository ?? throw new ArgumentNullException(nameof(attributeGroupRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Result<Guid>> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            // Validate request parameters
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<Guid>.InvalidArgument(nameof(request.Name), "Name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.AttributeCode))
                return Result<Guid>.InvalidArgument(nameof(request.AttributeCode), "Attribute code cannot be null or empty.");

            // Check if the attribute group exists
            var groupExists = await _attributeGroupRepository.GetByIdAsync(request.AttributeGroupId, cancellationToken);
            if (groupExists == null)
            {
                return Result<Guid>.NotFound(nameof(ProductAttributeGroup), "Attribute group not found.");
            }

            // Check if an attribute with the same name exists in the specified attribute group
            var attributeExists = await _attributeRepository.ExistsByNameAsync(request.Name, request.AttributeGroupId, cancellationToken);
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

                await _attributeRepository.AddAsync(attribute, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(attribute.Id);
            }
            catch (Exception ex)
            {
                // Return an unexpected error result
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
