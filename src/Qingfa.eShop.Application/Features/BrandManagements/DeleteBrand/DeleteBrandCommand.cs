using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace QingFa.EShop.Application.Features.BrandManagements.DeleteBrand
{
    public sealed record DeleteBrandCommand(Guid Id) : ICommand;

    internal class DeleteBrandCommandHandler : ICommandHandler<DeleteBrandCommand>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<DeleteBrandCommandHandler> _logger;

        public DeleteBrandCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<DeleteBrandCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteBrandCommand with ID: {Id}", request.Id);

            try
            {
                // Check if the brand exists
                var brand = await _dbProvider.Brands
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (brand == null)
                {
                    return Result.NotFound(nameof(Brand), "The brand you are trying to delete does not exist.");
                }

                // Delete the brand
                _dbProvider.Brands.Remove(brand);
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted brand with ID: {Id}", request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the brand with ID: {Id}", request.Id);
                return Result.UnexpectedError(ex);
            }
        }
    }
}
