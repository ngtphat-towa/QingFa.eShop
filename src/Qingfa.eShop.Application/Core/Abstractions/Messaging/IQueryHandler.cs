using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.Core.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}