using Ardalis.GuardClauses;

using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.ExampleMetas.Models;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class ListExampleMetasQueryHandler : IRequestHandler<ListExampleMetasQuery, PaginatedList<ExampleMetaResponse>>
    {
        private readonly IExampleMetaRepository _repository;

        public ListExampleMetasQueryHandler(IExampleMetaRepository repository)
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
            );

            // Get total count of items without paging
            var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

            // Apply paging parameters to the specification
            specification.ApplyPaging(request.PageNumber, request.PageSize);

            if (request.SortDescending)
            {
                specification.ApplyOrderByDescending(request.SortField);
            }
            else
            {
                specification.ApplyOrderBy(request.SortField);
            }

            // Fetch the data with the specification
            var exampleMetas = await _repository.FindBySpecificationAsync(specification, cancellationToken);

            // Map entities to response DTOs
            var exampleMetaResponses = exampleMetas.Adapt<List<ExampleMetaResponse>>();

            // Create and return paginated list of responses
            return new PaginatedList<ExampleMetaResponse>(exampleMetaResponses, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
