using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Enums;

using ICommand = QingFa.EShop.Application.Core.Abstractions.Messaging.ICommand;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup
{
    public record UpdateAttributeGroupCommand : RequestType<Guid>, ICommand
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public EntityStatus? Status { get; set; } = default!;
    }
    internal class UpdateAttributeGroupCommandHandler(
        IApplicationDbProvider applicationDbProvider) : ICommandHandler<UpdateAttributeGroupCommand>
    {
        private readonly IApplicationDbProvider _applicationDbProvider = applicationDbProvider
            ?? throw new ArgumentNullException(nameof(applicationDbProvider));

        public async Task<Result> Handle(UpdateAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attributeGroup = await _applicationDbProvider.ProductAttributeGroups
                  .FindAsync(new object[] { request.Id }, cancellationToken);
                if (attributeGroup == null)
                {
                    return Result.NotFound(nameof(ProductAttributeGroup), request.Id.ToString());
                }

                // Update properties
                attributeGroup.Update(request.Name,request.Description);

                if (request.Status.HasValue)
                {
                    attributeGroup.SetStatus(request.Status.Value);
                }

                // Save changes
                await _applicationDbProvider.SaveChangesAsync(cancellationToken);

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
