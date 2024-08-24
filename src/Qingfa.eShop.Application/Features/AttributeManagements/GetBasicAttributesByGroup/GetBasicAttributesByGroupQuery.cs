using MediatR;
using QingFa.EShop.Application.Features.AttributeManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using Mapster;

namespace QingFa.EShop.Application.Features.AttributeManagements.GetBasicAttributesByGroup
{
    public record GetBasicAttributesByGroupQuery : IRequest<List<BasicProductAttributeResponse>>
    {
        public Guid AttributeGroupId { get; init; }
        public string? SearchTerm { get; init; }
        public string SortField { get; init; } = "Name";
        public bool SortDescending { get; init; } = false;
    }

    internal class GetBasicAttributesByGroupQueryHandler : IRequestHandler<GetBasicAttributesByGroupQuery, List<BasicProductAttributeResponse>>
    {
        private readonly IProductAttributeRepository _attributeRepository;

        public GetBasicAttributesByGroupQueryHandler(IProductAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        }

        public async Task<List<BasicProductAttributeResponse>> Handle(GetBasicAttributesByGroupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var specification = new ProductAttributeSpecification(
                    attributeGroupId: request.AttributeGroupId,
                    searchTerm: request.SearchTerm,
                    includeAttributeOptions: true
                );

                // Apply sorting
                if (request.SortDescending)
                {
                    specification.ApplyOrderByDescending(request.SortField);
                }
                else
                {
                    specification.ApplyOrderBy(request.SortField);
                }

                // Fetch the data
                var attributes = await _attributeRepository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var attributeResponses = attributes.Adapt<List<BasicProductAttributeResponse>>();

                return attributeResponses;
            }
            catch (Exception ex)
            {
                // Handle exception and possibly log it
                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
