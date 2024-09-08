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

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.DeleteAttributeOption
{
    public record DeleteAttributeOptionCommand(Guid Id) : IRequest<Result>;

    internal class DeleteAttributeOptionCommandHandler : IRequestHandler<DeleteAttributeOptionCommand, Result>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<DeleteAttributeOptionCommandHandler> _logger;

        public DeleteAttributeOptionCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<DeleteAttributeOptionCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(DeleteAttributeOptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteAttributeOptionCommand with ID: {Id}", request.Id);

            if (request.Id == Guid.Empty)
            {
                _logger.LogWarning("Invalid attribute option ID: {Id}", request.Id);
                return Result.InvalidArgument(nameof(request.Id), "Invalid attribute option ID.");
            }

            try
            {
                // Check if the option exists
                var existingOption = await _dbProvider.ProductAttributeOptions
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (existingOption == null)
                {
                    _logger.LogWarning("Product attribute option not found with ID: {Id}", request.Id);
                    return Result.NotFound(nameof(ProductAttributeOption), $"No product attribute option found with ID: {request.Id}");
                }

                // Check if the option is in use
                var isInUse = await _dbProvider.ProductAttributeOptions
                    .AnyAsync(o => o.Id == request.Id && o.ProductAttributeId != null, cancellationToken);

                if (isInUse)
                {
                    _logger.LogWarning("Cannot delete attribute option ID: {Id} because it is in use.", request.Id);
                    return Result.Conflict(nameof(ProductAttributeOption), $"Option with ID: {request.Id} is in use and cannot be deleted.");
                }

                // Delete the option
                _dbProvider.ProductAttributeOptions.Remove(existingOption);

                // Save changes to the database
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted product attribute option with ID: {Id}", request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product attribute option with ID: {Id}", request.Id);
                return Result.UnexpectedError(ex);
            }
        }
    }
}
