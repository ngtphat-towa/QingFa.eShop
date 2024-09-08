using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.Core.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
