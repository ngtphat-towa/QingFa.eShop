using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.GetBrandById
{
    public record GetAttributeGroupById : IRequest<ResultValue<AttributeGroupResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAttributeGroupByIdQueryHandler : IRequestHandler<GetAttributeGroupById, ResultValue<AttributeGroupResponse>>
    {
        private readonly IProductAttributeGroupRepository _repository;

        public GetAttributeGroupByIdQueryHandler(IProductAttributeGroupRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultValue<AttributeGroupResponse>> Handle(GetAttributeGroupById request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return ResultValue<AttributeGroupResponse>.InvalidOperation("GetAttributeGroupById", "Invalid ID provided.");
            }

            var attributeGroup = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (attributeGroup == null)
            {
                return ResultValue<AttributeGroupResponse>.NotFound("attribute group", request.Id.ToString());
            }

            var response = attributeGroup.Adapt<AttributeGroupResponse>();
            return ResultValue<AttributeGroupResponse>.Success(response);
        }
    }
}
