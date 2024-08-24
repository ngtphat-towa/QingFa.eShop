using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.CreateAttributeOption
{
    public record CreateAttributeOptionCommand : IRequest<ResultValue<Guid>>
    {
        public string OptionValue { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid? ProductAttributeId { get; init; }
    }

    internal class CreateAttributeOptionCommandHandler : IRequestHandler<CreateAttributeOptionCommand, ResultValue<Guid>>
    {
        private readonly IProductAttributeOptionRepository _attributeOptionRepository;
        private readonly IProductAttributeRepository _attributeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttributeOptionCommandHandler(
            IProductAttributeOptionRepository attributeOptionRepository,
            IProductAttributeRepository attributeRepository,
            IUnitOfWork unitOfWork)
        {
            _attributeOptionRepository = attributeOptionRepository ?? throw new ArgumentNullException(nameof(attributeOptionRepository));
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResultValue<Guid>> Handle(CreateAttributeOptionCommand request, CancellationToken cancellationToken)
        {
            // Validate request parameters
            if (string.IsNullOrWhiteSpace(request.OptionValue))
                return ResultValue<Guid>.InvalidArgument(nameof(request.OptionValue), "Option value cannot be null or empty.");

            // Check if the product attribute exists if ProductAttributeId is provided
            if (request.ProductAttributeId.HasValue)
            {
                var attributeExists = await _attributeRepository.GetByIdAsync(request.ProductAttributeId.Value, cancellationToken);
                if (attributeExists == null)
                {
                    return ResultValue<Guid>.NotFound(nameof(ProductAttribute), "Product attribute not found.");
                }
            }

            // Check if an option with the same value exists for the specified product attribute
            var optionExists = await _attributeOptionRepository.ExistsByOptionValueAsync(request.OptionValue, request.ProductAttributeId, cancellationToken);
            if (optionExists)
            {
                return ResultValue<Guid>.Conflict(nameof(ProductAttributeOption), "An option with this value already exists for the specified product attribute.");
            }

            try
            {
                // Create the new product attribute option
                var option = ProductAttributeOption.Create(
                    optionValue: request.OptionValue,
                    description: request.Description,
                    isDefault: request.IsDefault,
                    sortOrder: request.SortOrder,
                    productAttributeId: request.ProductAttributeId ?? Guid.Empty
                );

                await _attributeOptionRepository.AddAsync(option, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultValue<Guid>.Success(option.Id);
            }
            catch (Exception ex)
            {
                return ResultValue<Guid>.UnexpectedError(ex);
            }
        }
    }
}
