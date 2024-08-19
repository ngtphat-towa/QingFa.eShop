using Ardalis.GuardClauses;
using MediatR;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.ExampleMetas.Delete
{
    public class DeleteExampleMetaCommandHandler : IRequestHandler<DeleteExampleMetaCommand, Result>
    {
        private readonly IGenericRepository<ExampleMeta, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExampleMetaCommandHandler(IGenericRepository<ExampleMeta, Guid> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Id, nameof(request.Id));

            var exampleMeta = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                return Result.Failure(new[] { $"ExampleMeta with ID {request.Id} not found." });
            }

            await _repository.DeleteAsync(exampleMeta, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
