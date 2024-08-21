using Ardalis.GuardClauses;
using MediatR;
using QingFa.EShop.Application.ExampleMetas.Gets;
using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Metas;
using Mapster;
using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Queries
{
    public class GetExampleMetaByIdQueryHandler : IRequestHandler<GetExampleMetaByIdQuery, ResultValue<ExampleMetaResponse>>
    {
        private readonly IExampleMetaRepository _repository;

        public GetExampleMetaByIdQueryHandler(IExampleMetaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultValue<ExampleMetaResponse>> Handle(GetExampleMetaByIdQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

            var exampleMeta = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                return ResultValue<ExampleMetaResponse>.NotFound("ExampleMeta");
            }

            var response = exampleMeta.Adapt<ExampleMetaResponse>();
            return ResultValue<ExampleMetaResponse>.Success(response);
        }
    }
}
