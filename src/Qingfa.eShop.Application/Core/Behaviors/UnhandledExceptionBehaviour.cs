using MediatR;

using Microsoft.Extensions.Logging;

namespace QingFa.EShop.Application.Core.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

        public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var requestData = request.ToString();

                _logger.LogError(
                    ex,
                    "Unhandled exception occurred for request {RequestName}. Request data: {@RequestData}",
                    requestName,
                    requestData
                );

                // Consider wrapping or rethrowing if needed
                throw;
            }
        }
    }
}
