using MediatR;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QingFa.EShop.Application.Features.AttributeManagements.DeleteAttribute
{
    public record DeleteProductAttributeCommand(Guid Id) : IRequest<Result<Guid>>;

    internal class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommand, Result<Guid>>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<DeleteProductAttributeCommandHandler> _logger;

        public DeleteProductAttributeCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<DeleteProductAttributeCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteProductAttributeCommand with ID: {Id}", request.Id);

            if (request.Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid product attribute ID: {Id}", request.Id);
                return Result<Guid>.InvalidArgument(nameof(request.Id), "Invalid product attribute ID.");
            }

            try
            {
                // Check if the attribute exists
                var existingAttribute = await _dbProvider.ProductAttributes
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (existingAttribute == null)
                {
                    _logger.LogWarning("Product attribute not found with ID: {Id}", request.Id);
                    return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.Id}");
                }

                // Check if the attribute is in use
                var isInUse = await _dbProvider.ProductAttributeOptions
                    .AnyAsync(o => o.ProductAttributeId == request.Id, cancellationToken);

                if (isInUse)
                {
                    _logger.LogWarning("Cannot delete attribute ID: {Id} because it is in use.", request.Id);
                    return Result<Guid>.Conflict(nameof(ProductAttribute), "The attribute cannot be deleted because it is in use.");
                }

                // Delete the product attribute
                _dbProvider.ProductAttributes.Remove(existingAttribute);

                // Save changes to the database
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted product attribute with ID: {Id}", request.Id);

                // Return the ID of the deleted attribute
                return Result<Guid>.Success(existingAttribute.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product attribute with ID: {Id}", request.Id);
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
