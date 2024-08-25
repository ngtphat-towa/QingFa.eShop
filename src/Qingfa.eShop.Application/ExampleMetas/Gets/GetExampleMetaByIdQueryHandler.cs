﻿using Ardalis.GuardClauses;
using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Metas;
using Mapster;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class GetExampleMetaByIdQueryHandler : IQueryHandler<GetExampleMetaByIdQuery, ExampleMetaResponse>
    {
        private readonly IExampleMetaRepository _repository;

        public GetExampleMetaByIdQueryHandler(IExampleMetaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ExampleMetaResponse>> Handle(GetExampleMetaByIdQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

            var exampleMeta = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                return Result<ExampleMetaResponse>.NotFound("ExampleMeta");
            }

            var response = exampleMeta.Adapt<ExampleMetaResponse>();
            return Result<ExampleMetaResponse>.Success(response);
        }
    }
}
