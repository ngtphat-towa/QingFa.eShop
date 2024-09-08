using Ardalis.GuardClauses;

using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Delete
{
    public class DeleteExampleMetaCommandHandler : ICommandHandler<DeleteExampleMetaCommand>
    {
        private readonly IApplicationDbProvider _dbContext;

        public DeleteExampleMetaCommandHandler(IApplicationDbProvider dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

            var exampleMeta = await _dbContext.ExampleMetas.FindAsync(new object[] { request.Id }, cancellationToken);
            if (exampleMeta == null)
            {
                return Result.Failure(new[] { $"ExampleMeta with ID {request.Id} not found." });
            }

            _dbContext.ExampleMetas.Remove(exampleMeta);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
