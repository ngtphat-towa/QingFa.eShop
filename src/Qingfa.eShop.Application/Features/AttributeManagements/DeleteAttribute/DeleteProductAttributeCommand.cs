using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeManagements.DeleteAttribute
{
    public record DeleteProductAttributeCommand(Guid Id) : IRequest<Result<Guid>>;

    internal class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommand, Result<Guid>>
    {
        private readonly IProductAttributeRepository _attributeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductAttributeCommandHandler(
            IProductAttributeRepository attributeRepository,
            IUnitOfWork unitOfWork)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Guid>> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the attribute exists
                var existingAttribute = await _attributeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingAttribute == null)
                {
                    return Result<Guid>.NotFound(nameof(ProductAttribute), $"No product attribute found with ID: {request.Id}");
                }

                // Check if the attribute is in use
                if (await _attributeRepository.IsInUseAsync(request.Id, cancellationToken))
                {
                    return Result<Guid>.Conflict(nameof(ProductAttribute), "The attribute cannot be deleted because it is in use.");
                }

                // Delete the product attribute
                await _attributeRepository.DeleteAsync(existingAttribute, cancellationToken);

                // Save changes to the database
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Return the ID of the deleted attribute
                return Result<Guid>.Success(existingAttribute.Id);
            }
            catch (Exception ex)
            {
                // Return an unexpected error result
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
