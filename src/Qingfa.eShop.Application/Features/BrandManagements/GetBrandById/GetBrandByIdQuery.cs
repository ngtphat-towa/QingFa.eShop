using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.BrandManagements.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<Result<BrandResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<BrandResponse>>
    {
        private readonly IBrandRepository _repository;

        public GetBrandByIdQueryHandler(IBrandRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return Result<BrandResponse>.InvalidOperation("GetBrandByIdQuery", "Invalid ID provided.");
            }
            var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (brand == null)
            {
                return Result<BrandResponse>.NotFound("Brand", request.Id.ToString());
            }

            var response = brand.Adapt<BrandResponse>();
            return Result<BrandResponse>.Success(response);
        }
    }
}
