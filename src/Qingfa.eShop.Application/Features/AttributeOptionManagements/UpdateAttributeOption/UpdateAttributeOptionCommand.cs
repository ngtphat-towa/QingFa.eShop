using MediatR;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using Microsoft.EntityFrameworkCore;
using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.UpdateAttributeOption
{
    public record UpdateAttributeOptionCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string OptionValue { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsDefault { get; init; }
        public int SortOrder { get; init; }
        public Guid ProductAttributeId { get; init; }
    }

    internal class UpdateAttributeOptionCommandHandler : ICommandHandler<UpdateAttributeOptionCommand, Guid>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<UpdateAttributeOptionCommandHandler> _logger;

        public UpdateAttributeOptionCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<UpdateAttributeOptionCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(UpdateAttributeOptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request parameters
                if (request.ProductAttributeId == Guid.Empty)
                {
                    return Result<Guid>.InvalidArgument(nameof(request.ProductAttributeId), "Product Attribute ID cannot be empty.");
                }

                // Check if the option exists
                var existingOption = await _dbProvider.ProductAttributeOptions
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (existingOption == null)
                {
                    return Result<Guid>.NotFound(nameof(ProductAttributeOption), $"No product attribute option found with ID: {request.Id}");
                }

                // Check if the product attribute exists
                var attributeExists = await _dbProvider.ProductAttributes
                    .FindAsync(new object[] { request.ProductAttributeId }, cancellationToken);

                if (attributeExists == null)
                {
                    return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.ProductAttributeId}");
                }

                // Check if the option is in use
                if (await _dbProvider.ProductVariantAttributes
                    .AnyAsync(pva => pva.ProductAttributeOptionId == request.Id, cancellationToken))
                {
                    return Result<Guid>.Conflict(nameof(ProductAttributeOption), $"Option with ID: {request.Id} is in use and cannot be updated.");
                }

                // Check if the ProductAttributeId is changing
                if (existingOption.ProductAttributeId != request.ProductAttributeId)
                {
                    // Check if the new ProductAttributeId is in use by other options
                    if (await _dbProvider.ProductAttributeOptions
                        .AnyAsync(o => o.OptionValue == existingOption.OptionValue &&
                                       o.ProductAttributeId == request.ProductAttributeId, cancellationToken))
                    {
                        return Result<Guid>.Conflict(nameof(ProductAttributeOption), $"The new product attribute ID: {request.ProductAttributeId} is already in use.");
                    }

                    // Check if the old ProductAttributeId is in use by other options
                    if (await _dbProvider.ProductAttributeOptions
                        .AnyAsync(o => o.OptionValue == existingOption.OptionValue &&
                                       o.ProductAttributeId == existingOption.ProductAttributeId, cancellationToken))
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
                await _dbProvider.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(existingOption.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the attribute option.");
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
