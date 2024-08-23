using MediatR;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup
{
    public record CreateAttributeGroupCommand : IRequest<ResultValue<Guid>>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    internal class CreateAttributeGroupCommandHandler(
        IProductAttributeGroupRepository attributeGroupRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateAttributeGroupCommand, ResultValue<Guid>>
    {
        private readonly IProductAttributeGroupRepository _attributeGroupRepository = attributeGroupRepository ?? throw CoreException.NullArgument(nameof(attributeGroupRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));

        public async Task<ResultValue<Guid>> Handle(CreateAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if an attribute group with the same name already exists
                var attributeGroupExists = await _attributeGroupRepository.ExistsByNameAsync(request.Name, cancellationToken);
                if (attributeGroupExists)
                {
                    return ResultValue<Guid>.Conflict(nameof(ProductAttributeGroup), "An attribute group with this name already exists.");
                }

                // Create the new attribute group
                var attributeGroup = ProductAttributeGroup.Create(request.Name, request.Description);

                // Add the attribute group to the repository
                await _attributeGroupRepository.AddAsync(attributeGroup, cancellationToken);

                // Commit the transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultValue<Guid>.Success(attributeGroup.Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return ResultValue<Guid>.UnexpectedError(ex);
            }
        }
    }
}
