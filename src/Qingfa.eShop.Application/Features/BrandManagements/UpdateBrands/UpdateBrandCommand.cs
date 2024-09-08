using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Enums;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace QingFa.EShop.Application.Features.BrandManagements.Update
{
    public class UpdateBrandCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public EntityStatus? Status { get; set; }
    }

    internal class UpdateBrandCommandHandler : ICommandHandler<UpdateBrandCommand>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<UpdateBrandCommandHandler> _logger;

        public UpdateBrandCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<UpdateBrandCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateBrandCommand with ID: {Id}", request.Id);

            try
            {
                var brand = await _dbProvider.Brands
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (brand == null)
                {
                    return Result.NotFound(nameof(Brand), request.Id.ToString());
                }

                // Check for conflicting updates or conditions
                if (await _dbProvider.Brands.AnyAsync(b => b.Id != request.Id && b.Name == request.Name, cancellationToken))
                {
                    return Result.Conflict(nameof(Brand), "A brand with this name already exists.");
                }

                // Update properties
                brand.UpdateName(request.Name);
                brand.UpdateDescription(request.Description ?? string.Empty);
                brand.UpdateSeoMeta(SeoMeta.Create(
                    request.SeoMeta.Title ?? string.Empty,
                    request.SeoMeta.Description ?? string.Empty,
                    request.SeoMeta.Keywords ?? string.Empty,
                    request.SeoMeta.CanonicalUrl,
                    request.SeoMeta.Robots
                ));

                if (brand.Status != request.Status)
                {
                    brand.SetStatus(request.Status);
                }

                brand.UpdateLogoUrl(request.LogoUrl);

                _dbProvider.Brands.Update(brand);
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated brand with ID: {Id}", request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating brand with ID: {Id}", request.Id);
                return Result.UnexpectedError(ex);
            }
        }
    }
}
