using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Core.Behaviors
{
    public class LoggingBehavior<TRequest>(
        ILogger<LoggingBehavior<TRequest>> logger,
        ICurrentUser user,
        IIdentityService identityService) : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest>> _logger = logger ?? throw CoreException.NullArgument(nameof(logger));
        private readonly ICurrentUser _user = user ?? throw CoreException.NullArgument(nameof(user));
        private readonly IIdentityService _identityService = identityService ?? throw CoreException.NullArgument(nameof(identityService));

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId) ?? "Unknown";
            }

            _logger.LogInformation("Handling request: {RequestName} by User: {UserId} ({UserName}) {@Request}",
                requestName, userId ?? "Anonymous", userName, request);
        }
    }
}
