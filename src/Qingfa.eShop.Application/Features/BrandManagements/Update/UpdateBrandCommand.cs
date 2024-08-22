using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.BrandManagements.Update
{
    public class UpdateBrandCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
    }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
                if (brand == null)
                {
                    return Result.NotFound(nameof(Brand), request.Id.ToString());
                }

                // Check for conflicting updates or conditions
                if (await _brandRepository.ExistsByNameAsync(request.Name, cancellationToken))
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
                brand.UpdateLogoUrl(request.LogoUrl);

                await _brandRepository.UpdateAsync(brand, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
