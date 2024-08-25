using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.UpdateAttributeOption
{
    public record UpdateAttributeOptionCommand : RequestType<Guid>, IRequest<Result<Guid>>
    {
        public string OptionValue { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid ProductAttributeId { get; init; }
    }

    internal class UpdateProductAttributeOptionCommandHandler(
        IProductAttributeOptionRepository attributeOptionRepository,
        IProductAttributeRepository attributeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateAttributeOptionCommand, Result<Guid>>
    {
        private readonly IProductAttributeOptionRepository _attributeOptionRepository = attributeOptionRepository ?? throw new ArgumentNullException(nameof(attributeOptionRepository));
        private readonly IProductAttributeRepository _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Result<Guid>> Handle(UpdateAttributeOptionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                // Validate request parameters
                if (request.ProductAttributeId == Guid.Empty)
                    return Result<Guid>.InvalidArgument(nameof(request.ProductAttributeId), "Product Attribute ID cannot be empty.");

                // Check if the option exists
                var existingOption = await _attributeOptionRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingOption == null)
                    return Result<Guid>.NotFound(nameof(ProductAttributeOption), $"No product attribute option found with ID: {request.Id}");

                // Check if the product attribute exists
                var attributeExists = await _attributeRepository.GetByIdAsync(request.ProductAttributeId, cancellationToken);
                if (attributeExists == null)
                    return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.ProductAttributeId}");

                // Check if the option is in use
                if (await _attributeOptionRepository.IsInUseAsync(request.Id, cancellationToken))
                    return Result<Guid>.Conflict(nameof(ProductAttributeOption), $"Option with ID: {request.Id} is in use and cannot be updated.");

                // Check if the ProductAttributeId is changing
                if (existingOption.ProductAttributeId != request.ProductAttributeId)
                {
                    // Check if the new ProductAttributeId is in use by other options
                    if (await _attributeOptionRepository.ExistsByOptionValueAsync(existingOption.OptionValue, request.ProductAttributeId, cancellationToken))
                    {
                        return Result<Guid>.Conflict(nameof(ProductAttributeOption), $"The new product attribute ID: {request.ProductAttributeId} is already in use.");
                    }

                    // Check if the old ProductAttributeId is in use by other options
                    if (await _attributeOptionRepository.ExistsByOptionValueAsync(existingOption.OptionValue, existingOption.ProductAttributeId, cancellationToken))
                    {
                        return Result<Guid>.Conflict(nameof(ProductAttributeOption), $"The old product attribute ID: {existingOption.ProductAttributeId} is still in use.");
                    }
                }

                // Update the product attribute option
                existingOption.Update(
                    request.OptionValue,
                    request.Description,
                    request.IsDefault,
                    request.SortOrder);

                // If the ProductAttributeId has changed, update it
                if (existingOption.ProductAttributeId != request.ProductAttributeId)
                {
                    existingOption.UpdateProductAttributeId(request.ProductAttributeId);
                }

                // Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(existingOption.Id);
            }
            catch (Exception ex)
            {
                // Return an unexpected error result
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
