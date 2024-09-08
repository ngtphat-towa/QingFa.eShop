using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.CreateAttributeOption
{
    public record CreateAttributeOptionCommand : ICommand<Guid>
    {
        public string OptionValue { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid? ProductAttributeId { get; init; }
    }

    internal class CreateAttributeOptionCommandHandler : ICommandHandler<CreateAttributeOptionCommand, Guid>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<CreateAttributeOptionCommandHandler> _logger;

        public CreateAttributeOptionCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<CreateAttributeOptionCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(CreateAttributeOptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateAttributeOptionCommand with OptionValue: {OptionValue} and ProductAttributeId: {ProductAttributeId}",
                                    request.OptionValue, request.ProductAttributeId);

            // Validate request parameters
            if (string.IsNullOrWhiteSpace(request.OptionValue))
                return Result<Guid>.InvalidArgument(nameof(request.OptionValue), "Option value cannot be null or empty.");

            try
            {
                // Check if the product attribute exists if ProductAttributeId is provided
                if (request.ProductAttributeId.HasValue)
                {
                    var attribute = await _dbProvider.ProductAttributes
                        .FindAsync(new object[] { request.ProductAttributeId.Value }, cancellationToken);
                    if (attribute == null)
                    {
                        return Result<Guid>.NotFound(nameof(ProductAttribute), "Product attribute not found.");
                    }
                }

                // Check if an option with the same value exists for the specified product attribute
                var optionExists = await _dbProvider.ProductAttributeOptions
                    .AnyAsync(o => o.OptionValue.Equals(request.OptionValue, StringComparison.OrdinalIgnoreCase) &&
                                   (!request.ProductAttributeId.HasValue || o.ProductAttributeId == request.ProductAttributeId), cancellationToken);

                if (optionExists)
                {
                    return Result<Guid>.Conflict(nameof(ProductAttributeOption), "An option with this value already exists for the specified product attribute.");
                }

                // Create the new product attribute option
                var option = ProductAttributeOption.Create(
                    optionValue: request.OptionValue,
                    description: request.Description,
                    isDefault: request.IsDefault,
                    sortOrder: request.SortOrder,
                    productAttributeId: request.ProductAttributeId ?? Guid.Empty
                );

                await _dbProvider.ProductAttributeOptions.AddAsync(option, cancellationToken);
                await _dbProvider.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(option.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the attribute option with OptionValue: {OptionValue}", request.OptionValue);
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
