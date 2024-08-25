using Mapster;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.GetAttributeOptionById
{
    public record GetAttributeOptionByIdQuery(Guid Id) : IQuery<AttributeOptionResponse>;

    internal class GetAttributeOptionByIdQueryHandler(IProductAttributeOptionRepository attributeOptionRepository)
       : IQueryHandler<GetAttributeOptionByIdQuery, AttributeOptionResponse>
    {
        private readonly IProductAttributeOptionRepository _attributeOptionRepository = attributeOptionRepository ?? throw new ArgumentNullException(nameof(attributeOptionRepository));

        public async Task<Result<AttributeOptionResponse>> Handle(GetAttributeOptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var option = await _attributeOptionRepository.GetByIdAsync(request.Id, cancellationToken);

                if (option == null)
                {
                    return Result<AttributeOptionResponse>.NotFound(nameof(ProductAttributeOption), $"AttributeOption with ID {request.Id} not found.");
                }

                var response = option.Adapt<AttributeOptionResponse>();

                return Result<AttributeOptionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<AttributeOptionResponse>.UnexpectedError(ex);
            }
        }
    }
}
