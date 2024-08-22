using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.BrandManagements.Delete
{
    public sealed record DeleteBrandCommand : RequestType<Guid>, IRequest<Result>;

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
            if (brand == null)
            {
                return Result.NotFound(nameof(Brand), "The brand you are trying to delete does not exist.");
            }

            await _brandRepository.DeleteAsync(brand, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
