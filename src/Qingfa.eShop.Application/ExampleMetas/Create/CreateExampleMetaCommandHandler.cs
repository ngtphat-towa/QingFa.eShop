﻿using QingFa.EShop.Domain.Core.Repositories;
using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Domain.Metas;
using Ardalis.GuardClauses;
using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.ExampleMetas.Create
{
    public class CreateExampleMetaCommandHandler : ICommandHandler<CreateExampleMetaCommand, Guid>
    {
        private readonly IExampleMetaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateExampleMetaCommandHandler(IExampleMetaRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateExampleMetaCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, nameof(request.Name));
            Guard.Against.NullOrEmpty(request.CreatedBy, nameof(request.CreatedBy));

            var exampleMeta = new ExampleMeta(Guid.NewGuid(), request.Name, DateTimeOffset.UtcNow, request.CreatedBy);
            await _repository.AddAsync(exampleMeta, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(exampleMeta.Id); // Return the ID of the created entity
        }
    }
}
