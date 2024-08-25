using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

namespace QingFa.EShop.Application.Features.AttributeManagements.GetAttributeById
{
    public record GetProductAttributeByIdQuery(Guid Id) : IRequest<Result<ProductAttributeResponse>>;

    public class GetProductAttributeByIdQueryHandler : IRequestHandler<GetProductAttributeByIdQuery, Result<ProductAttributeResponse>>
    {
        private readonly IProductAttributeRepository _attributeRepository;

        public GetProductAttributeByIdQueryHandler(IProductAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        }

        public async Task<Result<ProductAttributeResponse>> Handle(GetProductAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the product attribute from the repository
                var attribute = await _attributeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (attribute == null)
                {
                    // Return a not found result if the attribute does not exist
                    return Result<ProductAttributeResponse>.NotFound(nameof(ProductAttribute), $"ProductAttribute with ID {request.Id} not found.");
                }

                // Map the ProductAttribute to ProductAttributeResponse
                var response = attribute.Adapt<ProductAttributeResponse>();

                // Return a success result with the mapped response
                return Result<ProductAttributeResponse>.Success(response);
            }
            catch (Exception ex)
            {
                // Handle and return any unexpected errors
                return Result<ProductAttributeResponse>.UnexpectedError(ex);
            }
        }
    }
}
