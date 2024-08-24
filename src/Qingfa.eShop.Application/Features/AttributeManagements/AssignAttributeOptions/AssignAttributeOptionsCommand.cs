using MediatR;

using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeManagements.AssignAttributeOptions
{
    public record AssignAttributeOptionsCommand : IRequest<Result>
    {
        public Guid ProductAttributeId { get; init; }
        public List<Guid> AttributeOptionIds { get; init; } = new List<Guid>();
    }

    internal class AssignAttributeOptionsCommandHandler : IRequestHandler<AssignAttributeOptionsCommand, Result>
    {
        private readonly IProductAttributeRepository _attributeRepository;
        private readonly IProductAttributeOptionRepository _optionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssignAttributeOptionsCommandHandler> _logger;

        public AssignAttributeOptionsCommandHandler(
            IProductAttributeRepository attributeRepository,
            IProductAttributeOptionRepository optionRepository,
            IUnitOfWork unitOfWork,
            ILogger<AssignAttributeOptionsCommandHandler> logger)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(AssignAttributeOptionsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AssignAttributeOptionsCommand with ProductAttributeId: {ProductAttributeId} and AttributeOptionIds: {AttributeOptionIds}",
                                    request.ProductAttributeId,
                                    string.Join(", ", request.AttributeOptionIds));

            if (request.ProductAttributeId == Guid.Empty)
            {
                _logger.LogWarning("Invalid product attribute ID: {ProductAttributeId}", request.ProductAttributeId);
                return Result.Failure("Invalid product attribute ID.");
            }

            if (!request.AttributeOptionIds.Any())
            {
                _logger.LogWarning("No attribute options selected.");
                return Result.Failure("No attribute options selected.");
            }

            try
            {
                // Fetch the product attribute
                var attribute = await _attributeRepository.GetByIdAsync(request.ProductAttributeId, cancellationToken);
                if (attribute == null)
                {
                    _logger.LogWarning("Product attribute not found: {ProductAttributeId}", request.ProductAttributeId);
                    return Result.NotFound(nameof(ProductAttribute), "The specified product attribute does not exist.");
                }

                // Fetch the attribute options using specifications
                var specification = new ProductAttributeOptionSpecification(
                    ids: request.AttributeOptionIds
                );
                var options = await _optionRepository.FindBySpecificationAsync(specification, cancellationToken);

                var optionIds = new HashSet<Guid>(request.AttributeOptionIds);
                var validOptions = options.Where(o => optionIds.Contains(o.Id)).ToList();
                var invalidOptionIds = optionIds.Except(validOptions.Select(o => o.Id)).ToList();

                if (invalidOptionIds.Any())
                {
                    _logger.LogWarning("Invalid attribute option IDs: {InvalidOptionIds}", string.Join(", ", invalidOptionIds));
                    return Result.NotFound(nameof(ProductAttributeOption), $"The following attribute options do not exist: {string.Join(", ", invalidOptionIds)}.");
                }

                var existingOptionIds = new HashSet<Guid>(attribute.AttributeOptions.Select(o => o.Id));
                var newOptions = validOptions.Where(o => !existingOptionIds.Contains(o.Id)).ToList();
                var optionsToRemove = attribute.AttributeOptions.Where(o => !optionIds.Contains(o.Id)).ToList();

                // Remove old options
                foreach (var option in optionsToRemove)
                {
                    attribute.AttributeOptions.Remove(option);
                    _logger.LogInformation("Removed attribute option ID: {AttributeOptionId} from product attribute ID: {ProductAttributeId}",
                                           option.Id,
                                           attribute.Id);
                }

                // Add new options
                foreach (var option in newOptions)
                {
                    attribute.AttributeOptions.Add(option);
                    _logger.LogInformation("Added attribute option ID: {AttributeOptionId} to product attribute ID: {ProductAttributeId}",
                                           option.Id,
                                           attribute.Id);
                }

                _logger.LogInformation("Updating product attribute.");
                await _attributeRepository.UpdateAsync(attribute);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling AssignAttributeOptionsCommand.");
                return Result.Conflict("Operation failed", "An unexpected error occurred.");
            }
        }
    }
}
