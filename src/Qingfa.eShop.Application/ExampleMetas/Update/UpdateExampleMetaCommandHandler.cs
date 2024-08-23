using Ardalis.GuardClauses;

using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Domain.Metas;

namespace QingFa.EShop.Application.ExampleMetas.Update
{
    public class UpdateExampleMetaCommandHandler : IRequestHandler<UpdateExampleMetaCommand, Result>
    {
        private readonly IExampleMetaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExampleMetaCommandHandler(IExampleMetaRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, nameof(request.Name));
            Guard.Against.NullOrEmpty(request.LastModifiedBy, nameof(request.LastModifiedBy));

            var exampleMeta = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (exampleMeta == null)
            {
                return Result.Failure(new[] { $"ExampleMeta with ID {request.Id} not found." });
            }

            exampleMeta.Update(request.Name, request.LastModifiedBy);
            await _repository.UpdateAsync(exampleMeta, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
