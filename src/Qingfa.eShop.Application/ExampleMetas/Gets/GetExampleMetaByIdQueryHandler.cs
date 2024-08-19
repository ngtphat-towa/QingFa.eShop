using Ardalis.GuardClauses;
using MediatR;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Application.ExampleMetas.Gets;
using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Metas;
using Mapster;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.ExampleMetas.Queries
{
    public class GetExampleMetaByIdQueryHandler : IRequestHandler<GetExampleMetaByIdQuery, ExampleMetaResponse>
    {
        private readonly IGenericRepository<ExampleMeta, Guid> _repository;

        public GetExampleMetaByIdQueryHandler(IGenericRepository<ExampleMeta, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ExampleMetaResponse> Handle(GetExampleMetaByIdQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

            var exampleMeta = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                throw CoreException.NotFound(nameof(request.Id));
            }

            var response = exampleMeta.Adapt<ExampleMetaResponse>();
            return response;
        }
    }
}
