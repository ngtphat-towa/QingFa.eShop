using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Interfaces;

namespace QingFa.EShop.Application.Core.Behaviors
{
    public class LoggingBehavior<TRequest>(
        ILogger<LoggingBehavior<TRequest>> logger,
        IUser user,
        IIdentityService identityService) : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IUser _user = user ?? throw new ArgumentNullException(nameof(user));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

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
