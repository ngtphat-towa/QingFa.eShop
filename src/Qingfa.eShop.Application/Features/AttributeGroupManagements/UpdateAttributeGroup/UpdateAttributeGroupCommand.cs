using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Catalogs.Repositories;
using QingFa.EShop.Domain.Core.Enums;
using QingFa.EShop.Domain.Core.Repositories;

using ICommand = QingFa.EShop.Application.Core.Abstractions.Messaging.ICommand;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup
{
    public record UpdateAttributeGroupCommand : RequestType<Guid>, ICommand
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public EntityStatus? Status { get; set; } = default!;
    }
    internal class UpdateAttributeGroupCommandHandler(IProductAttributeGroupRepository attributeGroupRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateAttributeGroupCommand>
    {
        private readonly IProductAttributeGroupRepository _attributeGroupRepository = attributeGroupRepository ?? throw new ArgumentNullException(nameof(attributeGroupRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Result> Handle(UpdateAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attributeGroup = await _attributeGroupRepository.GetByIdAsync(request.Id, cancellationToken);
                if (attributeGroup == null)
                {
                    return Result.NotFound(nameof(ProductAttributeGroup), request.Id.ToString());
                }

                // Update properties
                attributeGroup.Update(request.Name,request.Description);

                attributeGroup.SetStatus(request.Status);

                await _attributeGroupRepository.UpdateAsync(attributeGroup, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return Result.UnexpectedError(ex);
            }
        }
    }
}
