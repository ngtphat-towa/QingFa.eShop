﻿using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.ListAttributeOptions
{
    public record ListAttributeOptionsQuery : IRequest<PaginatedList<AttributeOptionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OptionValue { get; set; }
        public string? Description { get; set; }
        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }
        public Guid? ProductAttributeId { get; set; }
        public string SortField { get; set; } = "OptionValue";
        public bool SortDescending { get; set; }
        public List<Guid>? Ids { get; set; }
        public string? SearchTerm { get; set; }
    }

    internal class ListAttributeOptionsQueryHandler : IRequestHandler<ListAttributeOptionsQuery, PaginatedList<AttributeOptionResponse>>
    {
        private readonly IProductAttributeOptionRepository _repository;

        public ListAttributeOptionsQueryHandler(IProductAttributeOptionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<PaginatedList<AttributeOptionResponse>> Handle(ListAttributeOptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Create a specification based on the query parameters
                var specification = new ProductAttributeOptionSpecification(
                    ids: request.Ids,
                    optionValue: request.OptionValue,
                    description: request.Description,
                    isDefault: request.IsDefault,
                    sortOrder: request.SortOrder,
                    productAttributeId: request.ProductAttributeId,
                    searchTerm: request.SearchTerm
                );

                // Get total count of items without paging
                var totalCount = await _repository.CountBySpecificationAsync(specification, cancellationToken);

                // Apply paging parameters to the specification
                specification.ApplyPaging(request.PageNumber, request.PageSize);

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
                var options = await _repository.FindBySpecificationAsync(specification, cancellationToken);

                // Map entities to response DTOs
                var optionResponses = options.Adapt<List<AttributeOptionResponse>>();

                // Create and return paginated list of responses
                var paginatedList = new PaginatedList<AttributeOptionResponse>(
                    optionResponses,
                    totalCount,
                    request.PageNumber,
                    request.PageSize
                );

                return paginatedList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
