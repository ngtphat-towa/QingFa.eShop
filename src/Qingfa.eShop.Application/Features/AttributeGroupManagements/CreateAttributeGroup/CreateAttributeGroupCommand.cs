using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup
{
    public record CreateAttributeGroupCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    internal class CreateAttributeGroupCommandHandler(
        IProductAttributeGroupRepository attributeGroupRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateAttributeGroupCommand, Result<Guid>>
    {
        private readonly IProductAttributeGroupRepository _attributeGroupRepository = attributeGroupRepository ?? throw CoreException.NullArgument(nameof(attributeGroupRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));

        public async Task<Result<Guid>> Handle(CreateAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attributeGroupExists = await _attributeGroupRepository.ExistsByNameAsync(request.Name, cancellationToken);
                if (attributeGroupExists)
                {
                    return Result<Guid>.Conflict(nameof(ProductAttributeGroup), "An attribute group with this name already exists.");
                }

                var attributeGroup = ProductAttributeGroup.Create(request.Name, request.Description);

                await _attributeGroupRepository.AddAsync(attributeGroup, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(attributeGroup.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
