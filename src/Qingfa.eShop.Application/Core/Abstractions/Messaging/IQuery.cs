using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.Core.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}