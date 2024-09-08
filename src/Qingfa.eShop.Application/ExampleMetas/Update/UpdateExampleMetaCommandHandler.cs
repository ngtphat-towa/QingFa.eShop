using Ardalis.GuardClauses;

using MediatR;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Update
{
    public class UpdateExampleMetaCommandHandler : IRequestHandler<UpdateExampleMetaCommand, Result>
    {
        private readonly IApplicationDbProvider _dbContext;

        public UpdateExampleMetaCommandHandler(IApplicationDbProvider dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, nameof(request.Name));
            Guard.Against.NullOrEmpty(request.LastModifiedBy, nameof(request.LastModifiedBy));

            var exampleMeta = await _dbContext.ExampleMetas.FindAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                return Result.Failure(new[] { $"ExampleMeta with ID {request.Id} not found." });
            }

            exampleMeta.Update(request.Name, request.LastModifiedBy);
            _dbContext.ExampleMetas.Update(exampleMeta);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
