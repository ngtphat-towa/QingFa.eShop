using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Features.Common.Requests;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.DeleteAttributeGroup
{
    public record DeleteAttributeGroupCommand : RequestType<Guid>, ICommand;
    internal class DeleteAttributeGroupCommandHandler(
       IApplicationDbProvider dbContext) : ICommand<DeleteAttributeGroupCommand>
    {
        private readonly IApplicationDbProvider _dbContext = dbContext
            ?? throw CoreException.NullArgument(nameof(dbContext));

        public async Task<Result> Handle(DeleteAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            // Fetch the ProductAttributeGroup entity by ID
            var attributeGroup = await _dbContext.ProductAttributeGroups
                .FindAsync(new object[] { request.Id }, cancellationToken);

            // Check if the entity exists
            if (attributeGroup == null)
            {
                return Result.NotFound(nameof(ProductAttributeGroup), "The attribute group you are trying to delete does not exist.");
            }

            // Remove the entity from the context
            _dbContext.ProductAttributeGroups.Remove(attributeGroup);

            // Save changes
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
