using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories.Attributes;
using QingFa.EShop.Domain.Core.Repositories;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.DeleteAttributeOption
{
    public record DeleteAttributeOptionCommand(Guid Id) : IRequest<Result>;

    internal class DeleteAttributeOptionCommandHandler(
        IProductAttributeOptionRepository attributeOptionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAttributeOptionCommand, Result>
    {
        private readonly IProductAttributeOptionRepository _attributeOptionRepository = attributeOptionRepository ?? throw new ArgumentNullException(nameof(attributeOptionRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Result> Handle(DeleteAttributeOptionCommand request, CancellationToken cancellationToken)
        {
            // Check if the option exists
            var existingOption = await _attributeOptionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingOption == null)
                return Result.NotFound(nameof(ProductAttributeOption), $"No product attribute option found with ID: {request.Id}");

            // Check if the option is in use
            if (await _attributeOptionRepository.IsInUseAsync(request.Id, cancellationToken))
                return Result.Conflict(nameof(ProductAttributeOption), $"Option with ID: {request.Id} is in use and cannot be deleted.");

            try
            {
                // Delete the option
                await _attributeOptionRepository.DeleteAsync(existingOption, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Return an unexpected error result
                return Result.UnexpectedError(ex);
            }
        }
    }
}
