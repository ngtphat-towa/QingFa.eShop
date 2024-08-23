using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.DeleteAttributeGroup
{
    public record DeleteAttributeGroupCommand : RequestType<Guid>, IRequest<Result>;
    internal class DeleteAttributeGroupCommandHandler(
        IProductAttributeGroupRepository productAttributeGroupRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAttributeGroupCommand, Result>
    {
        private readonly IProductAttributeGroupRepository _productAttributeGroupRepository = productAttributeGroupRepository
                               ?? throw CoreException.NullArgument(nameof(productAttributeGroupRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork
                ?? throw CoreException.NullArgument(nameof(unitOfWork));

        public async Task<Result> Handle(DeleteAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            var brand = await _productAttributeGroupRepository.GetByIdAsync(request.Id, cancellationToken);
            if (brand == null)
            {
                return Result.NotFound(nameof(ProductAttributeGroup), "The attribute group you are trying to delete does not exist.");
            }

            await _productAttributeGroupRepository.DeleteAsync(brand, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
