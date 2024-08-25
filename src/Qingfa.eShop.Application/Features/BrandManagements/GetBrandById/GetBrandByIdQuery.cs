using Mapster;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.BrandManagements.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.BrandManagements.GetBrandById
{
    public record GetBrandByIdQuery : RequestType<Guid>, IQuery<BrandResponse>;

    internal class GetBrandByIdQueryHandler(IBrandRepository repository) : IQueryHandler<GetBrandByIdQuery, BrandResponse>
    {
        private readonly IBrandRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

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
