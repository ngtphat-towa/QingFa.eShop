using MediatR;
using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Metas;
using Ardalis.GuardClauses;

namespace QingFa.EShop.Application.ExampleMetas.Create
{
    public class CreateExampleMetaCommandHandler : IRequestHandler<CreateExampleMetaCommand, ResultValue<Guid>>
    {
        private readonly IGenericRepository<ExampleMeta, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateExampleMetaCommandHandler(IGenericRepository<ExampleMeta, Guid> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultValue<Guid>> Handle(CreateExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, nameof(request.Name));
            Guard.Against.NullOrEmpty(request.CreatedBy, nameof(request.CreatedBy));

            var exampleMeta = new ExampleMeta(Guid.NewGuid(), request.Name, DateTimeOffset.UtcNow, request.CreatedBy);
            await _repository.AddAsync(exampleMeta, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultValue<Guid>.Success(exampleMeta.Id); // Return the ID of the created entity
        }
    }
}
