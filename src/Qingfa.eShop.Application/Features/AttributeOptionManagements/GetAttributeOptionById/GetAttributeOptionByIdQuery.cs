using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeOptionManagements.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.GetAttributeOptionById
{
    public record GetAttributeOptionByIdQuery(Guid Id) : IRequest<ResultValue<AttributeOptionResponse>>;

     internal class GetAttributeOptionByIdQueryHandler : IRequestHandler<GetAttributeOptionByIdQuery, ResultValue<AttributeOptionResponse>>
    {
        private readonly IProductAttributeOptionRepository _attributeOptionRepository;

        public GetAttributeOptionByIdQueryHandler(IProductAttributeOptionRepository attributeOptionRepository)
        {
            _attributeOptionRepository = attributeOptionRepository ?? throw new ArgumentNullException(nameof(attributeOptionRepository));
        }

        public async Task<ResultValue<AttributeOptionResponse>> Handle(GetAttributeOptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var option = await _attributeOptionRepository.GetByIdAsync(request.Id, cancellationToken);

                if (option == null)
                {
                    return ResultValue<AttributeOptionResponse>.NotFound(nameof(ProductAttributeOption), $"AttributeOption with ID {request.Id} not found.");
                }

                var response = option.Adapt<AttributeOptionResponse>();

                return ResultValue<AttributeOptionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return ResultValue<AttributeOptionResponse>.UnexpectedError(ex);
            }
        }
    }
}
