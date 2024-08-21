using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.BrandManagements.Get
{
    public class GetBrandByIdQuery : IRequest<ResultValue<BrandResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, ResultValue<BrandResponse>>
    {
        private readonly IBrandRepository _repository;

        public GetBrandByIdQueryHandler(IBrandRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultValue<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return ResultValue<BrandResponse>.InvalidOperation("GetBrandByIdQuery", "Invalid ID provided.");
            }
            var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (brand == null)
            {
                return ResultValue<BrandResponse>.NotFound("Brand", request.Id.ToString());
            }

            var response = brand.Adapt<BrandResponse>();
            return ResultValue<BrandResponse>.Success(response);
        }
    }
}
