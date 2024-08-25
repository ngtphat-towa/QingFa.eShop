using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeManagements.UpdateAttribute
{
    public record UpdateProductAttributeCommand : RequestType<Guid>, IRequest<Result<Guid>>
    {
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
        private readonly IProductAttributeRepository _attributeRepository;
        private readonly IProductAttributeGroupRepository _attributeGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductAttributeCommandHandler(
            IProductAttributeRepository attributeRepository,
            IProductAttributeGroupRepository attributeGroupRepository,
            IUnitOfWork unitOfWork)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _attributeGroupRepository = attributeGroupRepository ?? throw new ArgumentNullException(nameof(attributeGroupRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Guid>> Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            // Validate request parameters
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<Guid>.InvalidArgument(nameof(request.Name), "Name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(request.AttributeCode))
                return Result<Guid>.InvalidArgument(nameof(request.AttributeCode), "Attribute code cannot be null or empty.");

            // Check if the attribute exists
            var existingAttribute = await _attributeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingAttribute == null)
                return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.Id}");

            // Check if the attribute group exists
            var attributeGroupExists = await _attributeGroupRepository.GetByIdAsync(request.AttributeGroupId, cancellationToken);
            if (attributeGroupExists == null)
                return Result<Guid>.NotFound(nameof(ProductAttributeGroup), $"No attribute group found with ID: {request.AttributeGroupId}");

            try
            {
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

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(existingAttribute.Id);
            }
            catch (Exception ex)
            {
                // Return an unexpected error result
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
