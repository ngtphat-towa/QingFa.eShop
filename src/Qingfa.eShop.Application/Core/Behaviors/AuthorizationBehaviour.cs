using System.Reflection;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using QingFa.EShop.Application.Core.Interfaces;
using QingFa.EShop.Application.Core.Exceptions;

namespace QingFa.EShop.Application.Core.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUser _user;
        private readonly IIdentityService _identityService;
        private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;

        public AuthorizationBehavior(IUser user, IIdentityService identityService, ILogger<AuthorizationBehavior<TRequest, TResponse>> logger)
        {
            _user = Guard.Against.Null(user, nameof(user));
            _identityService = Guard.Against.Null(identityService, nameof(identityService));
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling request of type {RequestType}.", typeof(TRequest).Name);

                var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>(inherit: true);

                if (authorizeAttributes.Any())
                {
                    _logger.LogInformation("Authorizing user with request of type {RequestType}.", typeof(TRequest).Name);

                    // Concurrently handle authorization checks
                    await AuthorizeUserAsync(authorizeAttributes, cancellationToken);

                    _logger.LogInformation("User authorized successfully.");
                }
                else
                {
                    _logger.LogInformation("No authorization attributes found. Proceeding to the next handler.");
                }

                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authorization for request of type {RequestType}.", typeof(TRequest).Name);
                throw new AuthorizationException("An error occurred during authorization.", "An error occurred while handling authorization.", ex);
            }
        }

        private async Task AuthorizeUserAsync(IEnumerable<AuthorizeAttribute> authorizeAttributes, CancellationToken cancellationToken)
        {
            try
            {
                // Ensure _user.Id is not null or empty
                var userId = Guard.Against.NullOrWhiteSpace(_user.Id, nameof(_user.Id));

                // Concurrent role-based and policy-based authorization
                var roleAttributes = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
                    .ToList();

                var policyAttributes = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                    .ToList();

                var roleCheckTask = roleAttributes.Any()
                    ? CheckUserRolesAsync(userId, roleAttributes, cancellationToken)
                    : Task.CompletedTask;

                var policyCheckTask = policyAttributes.Any()
                    ? CheckUserPoliciesAsync(userId, policyAttributes, cancellationToken)
                    : Task.CompletedTask;

                await Task.WhenAll(roleCheckTask, policyCheckTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user authorization.");
                throw new AuthorizationException("An error occurred during user authorization.", "An error occurred while authorizing user.", ex);
            }
        }

        private async Task CheckUserRolesAsync(string userId, IEnumerable<AuthorizeAttribute> roleAttributes, CancellationToken cancellationToken)
        {
            foreach (var role in roleAttributes
                .SelectMany(a => a.Roles!.Split(',').Select(r => r.Trim())))
            {
                Guard.Against.NullOrWhiteSpace(role, nameof(role));

                // Check if user is in role
                if (await _identityService.IsInRoleAsync(userId, role))
                {
                    return;
                }
            }
            _logger.LogWarning("User is not authorized for role-based access.");
            throw PermissionException.Denied("role-based authorization");
        }

        private async Task CheckUserPoliciesAsync(string userId, IEnumerable<AuthorizeAttribute> policyAttributes, CancellationToken cancellationToken)
        {
            foreach (var policy in policyAttributes.Select(a => a.Policy))
            {
                Guard.Against.NullOrWhiteSpace(policy, nameof(policy));

                // Check if user is authorized with policy
                if (!await _identityService.AuthorizeAsync(userId, policy))
                {
                    _logger.LogWarning("User is not authorized for policy-based access with policy: {Policy}.", policy);
                    throw PermissionException.Denied($"policy-based authorization with policy: {policy}");
                }
            }
        }
    }
}
