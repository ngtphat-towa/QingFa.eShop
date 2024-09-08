using Ardalis.GuardClauses;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Metas;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace QingFa.EShop.Application.ExampleMetas.Create
{
    public class CreateExampleMetaCommandHandler : ICommandHandler<CreateExampleMetaCommand, Guid>
    {
        private readonly IApplicationDbProvider _dbContext;

        public CreateExampleMetaCommandHandler(IApplicationDbProvider dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(CreateExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, nameof(request.Name));
            Guard.Against.NullOrEmpty(request.CreatedBy, nameof(request.CreatedBy));

            var exampleMeta = new ExampleMeta(Guid.NewGuid(), request.Name, DateTimeOffset.UtcNow, request.CreatedBy);

            await _dbContext.ExampleMetas.AddAsync(exampleMeta, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(exampleMeta.Id);
        }
    }
}
