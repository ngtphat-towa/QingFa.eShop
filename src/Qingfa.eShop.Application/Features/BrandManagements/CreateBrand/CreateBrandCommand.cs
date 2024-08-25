using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.SeoInfo;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Common.ValueObjects;
using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.BrandManagements.CreateBrand
{
    public class CreateBrandCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public SeoMetaTransfer SeoMeta { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public EntityStatus? Status { get; set; }
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<Guid>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a brand with the same name already exists
                if (await _brandRepository.ExistsByNameAsync(request.Name, cancellationToken))
                {
                    return Result<Guid>.Conflict(nameof(Brand), "A brand with this name already exists.");
                }

                var seoMeta = SeoMeta.Create(
                    request.SeoMeta.Title ?? string.Empty,
                    request.SeoMeta.Description ?? string.Empty,
                    request.SeoMeta.Keywords ?? string.Empty,
                    request.SeoMeta.CanonicalUrl,
                    request.SeoMeta.Robots);

                var brand = Brand.Create(request.Name, request.Description, seoMeta, request.LogoUrl);

                brand.SetStatus(request.Status);

                await _brandRepository.AddAsync(brand, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(brand.Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
