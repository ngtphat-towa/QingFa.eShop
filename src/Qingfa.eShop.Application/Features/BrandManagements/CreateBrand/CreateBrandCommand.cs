using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.BrandManagements.CreateBrand
{
    public class CreateBrandCommand : ICommand<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public EntityStatus? Status { get; set; }
    }

    internal class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, Guid>
    {
        private readonly IApplicationDbProvider _dbProvider;
        private readonly ILogger<CreateBrandCommandHandler> _logger;

        public CreateBrandCommandHandler(
            IApplicationDbProvider dbProvider,
            ILogger<CreateBrandCommandHandler> logger)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateBrandCommand with Name: {Name}", request.Name);

            try
            {
                // Check if a brand with the same name already exists
                var brandExists = await _dbProvider.Brands
                    .AnyAsync(b => b.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), cancellationToken);

                if (brandExists)
                {
                    return Result<Guid>.Conflict(nameof(Brand), "A brand with this name already exists.");
                }

                // Create the SEO metadata
                var seoMeta = SeoMeta.Create(
                    request.SeoMeta.Title ?? string.Empty,
                    request.SeoMeta.Description ?? string.Empty,
                    request.SeoMeta.Keywords ?? string.Empty,
                    request.SeoMeta.CanonicalUrl,
                    request.SeoMeta.Robots);

                // Create the new brand
                var brand = Brand.Create(request.Name, request.Description, seoMeta, request.LogoUrl);

                // Set the status if provided
                if (request.Status.HasValue)
                {
                    brand.SetStatus(request.Status.Value);
                }

                // Add the brand to the context
                await _dbProvider.Brands.AddAsync(brand, cancellationToken);
                await _dbProvider.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully created brand with ID: {Id}", brand.Id);

                return Result<Guid>.Success(brand.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the brand with Name: {Name}", request.Name);
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
