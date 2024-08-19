using Ardalis.GuardClauses;

using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.ExampleMetas.Gets;
using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.ExampleMetas.Queries
{
    public class ListExampleMetasQueryHandler : IRequestHandler<ListExampleMetasQuery, PaginatedList<ExampleMetaResponse>>
    {
        private readonly IGenericRepository<ExampleMeta, Guid> _repository;

        public ListExampleMetasQueryHandler(IGenericRepository<ExampleMeta, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ExampleMetaResponse>> Handle(ListExampleMetasQuery request, CancellationToken cancellationToken)
        {
            // Validate request
            Guard.Against.NegativeOrZero(request.PageNumber, nameof(request.PageNumber));
            Guard.Against.NegativeOrZero(request.PageSize, nameof(request.PageSize));

            // Create a new specification with filters
            var specification = new ExampleMetaSpecification(
                name: request.Name,
                id: request.Id,
                createdBy: request.CreatedBy
            )
            {
                Skip = (request.PageNumber - 1) * request.PageSize,
                Take = request.PageSize
            };

            if (request.SortDescending)
            {
                specification.ApplyOrderByDescending(request.SortField);
            }
            else
            {
                specification.ApplyOrderBy(request.SortField);
            }

            // Fetch the data with the specification
            var exampleMetas = await _repository.ListAsync(specification, cancellationToken);

            // Convert IEnumerable to List for pagination
            var exampleMetaList = exampleMetas.ToList();

            // Apply pagination
            var paginatedExampleMetas = PaginatedList<ExampleMeta>.Create(exampleMetaList, request.PageNumber, request.PageSize);

            // Map entities to response DTOs
            var exampleMetaResponses = paginatedExampleMetas.Items.Adapt<List<ExampleMetaResponse>>();

            // Return paginated list of responses
            return new PaginatedList<ExampleMetaResponse>(exampleMetaResponses, paginatedExampleMetas.TotalCount, request.PageNumber, request.PageSize);
        }

      
    }
}
