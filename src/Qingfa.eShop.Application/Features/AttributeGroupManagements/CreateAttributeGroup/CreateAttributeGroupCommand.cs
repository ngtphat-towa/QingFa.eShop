using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Catalogs.Entities.Attributes;
using QingFa.EShop.Domain.Core.Exceptions;
using QingFa.EShop.Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup
{
    public record CreateAttributeGroupCommand : ICommand<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    internal class CreateAttributeGroupCommandHandler(
        IApplicationDbProvider dbContext) : ICommandHandler<CreateAttributeGroupCommand, Guid>
    {
        private readonly IApplicationDbProvider _dbContext = dbContext ?? throw CoreException.NullArgument(nameof(dbContext));

        public async Task<Result<Guid>> Handle(CreateAttributeGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attributeGroupExists = await _dbContext.ProductAttributeGroups
                    .AnyAsync(ag => ag.GroupName == request.Name, cancellationToken);

                if (attributeGroupExists)
                {
                    return Result<Guid>.Conflict(nameof(ProductAttributeGroup), "An attribute group with this name already exists.");
                }

                var attributeGroup = ProductAttributeGroup.Create(request.Name, request.Description);

                await _dbContext.ProductAttributeGroups.AddAsync(attributeGroup, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(attributeGroup.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.UnexpectedError(ex);
            }
        }
    }
}
