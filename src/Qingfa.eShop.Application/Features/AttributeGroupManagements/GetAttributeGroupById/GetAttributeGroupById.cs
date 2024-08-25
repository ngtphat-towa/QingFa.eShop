using Mapster;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.AttributeGroupManagements.Models;
using QingFa.EShop.Domain.Catalogs.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.GetBrandById
{
    public record GetAttributeGroupById : IRequest<Result<AttributeGroupResponse>>
    {
        public Guid Id { get; set; }
    }

    internal class GetAttributeGroupByIdQueryHandler : IRequestHandler<GetAttributeGroupById, Result<AttributeGroupResponse>>
    {
        private readonly IProductAttributeGroupRepository _repository;

        public GetAttributeGroupByIdQueryHandler(IProductAttributeGroupRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<AttributeGroupResponse>> Handle(GetAttributeGroupById request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return Result<AttributeGroupResponse>.InvalidOperation("GetAttributeGroupById", "Invalid ID provided.");
            }

            var attributeGroup = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (attributeGroup == null)
            {
                return Result<AttributeGroupResponse>.NotFound("attribute group", request.Id.ToString());
            }

            var response = attributeGroup.Adapt<AttributeGroupResponse>();
            return Result<AttributeGroupResponse>.Success(response);
        }
    }
}
