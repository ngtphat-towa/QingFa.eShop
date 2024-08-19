using System.Diagnostics;

using MediatR;

using Microsoft.Extensions.Logging;

using QingFa.EShop.Application.Core.Interfaces;

namespace QingFa.EShop.Application.Core.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUser _user;
        private readonly IIdentityService _identityService;

        public PerformanceBehavior(
            ILogger<TRequest> logger,
            ICurrentUser user,
            IIdentityService identityService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew(); // Start the stopwatch

            // Create a task to get the user name asynchronously
            Task<string?> userNameTask = !string.IsNullOrEmpty(_user.Id)
                ? _identityService.GetUserNameAsync(_user.Id)
                : Task.FromResult<string?>(string.Empty);

            // Execute the request
            var responseTask = next();

            // Wait for both tasks to complete
            await Task.WhenAll(responseTask, userNameTask);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // Log if elapsed time exceeds threshold
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _user.Id;
                var userName = await userNameTask ?? "Unknown"; 

                _logger.LogWarning(
                    "Long Running Request: {RequestName} took {ElapsedMilliseconds} ms. User ID: {UserId}, User Name: {UserName}. Request Details: {@Request}",
                    requestName,
                    elapsedMilliseconds,
                    userId ?? "Anonymous",
                    userName,
                    request
                );
            }

            return await responseTask;
        }
    }
}
