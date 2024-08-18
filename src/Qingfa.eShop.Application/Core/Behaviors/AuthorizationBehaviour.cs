using System.Reflection;
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
                    await AuthorizeUserAsync(authorizeAttributes);

                    // User is authorized / authorization not required
                    _logger.LogInformation("User authorized successfully.");
                    return await next();
                }

                // No authorization attributes - continue with the next handler
                _logger.LogInformation("No authorization attributes found. Proceeding to the next handler.");
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authorization for request of type {RequestType}.", typeof(TRequest).Name);
                throw new AuthorizationException("An error occurred during authorization.", "An error occurred while handling authorization.", ex);
            }
        }

        private async Task AuthorizeUserAsync(IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            try
            {
                // Ensure _user.Id is not null or empty
                Guard.Against.NullOrWhiteSpace(_user.Id, nameof(_user.Id));

                // Role-based authorization
                var rolesAttributes = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
                    .ToList();

                if (rolesAttributes.Any())
                {
                    var isAuthorized = await IsUserInAnyRoleAsync(rolesAttributes);
                    if (!isAuthorized)
                    {
                        _logger.LogWarning("User is not authorized for role-based access.");
                        throw PermissionException.Denied("role-based authorization");
                    }
                }

                // Policy-based authorization
                var policiesAttributes = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                    .ToList();

                if (policiesAttributes.Any())
                {
                    await AuthorizeUserWithPoliciesAsync(policiesAttributes);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user authorization.");
                throw new AuthorizationException("An error occurred during user authorization.", "An error occurred while authorizing user.", ex);
            }
        }

        private async Task<bool> IsUserInAnyRoleAsync(IEnumerable<AuthorizeAttribute> roleAttributes)
        {
            try
            {
                // Ensure _user.Id is not null or empty
                Guard.Against.NullOrWhiteSpace(_user.Id, nameof(_user.Id));

                foreach (var role in roleAttributes
                    .SelectMany(a => a.Roles!.Split(',').Select(r => r.Trim())))
                {
                    Guard.Against.NullOrWhiteSpace(role, nameof(role));

                    // Check if user is in role
                    if (await _identityService.IsInRoleAsync(_user.Id, role))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking user roles.");
                throw new AuthorizationException("An error occurred while checking user roles.", "An error occurred while checking user roles.", ex);
            }
        }

        private async Task AuthorizeUserWithPoliciesAsync(IEnumerable<AuthorizeAttribute> policyAttributes)
        {
            try
            {
                foreach (var policy in policyAttributes.Select(a => a.Policy))
                {
                    Guard.Against.NullOrWhiteSpace(policy, nameof(policy));

                    // Ensure _user.Id is not null or empty
                    Guard.Against.NullOrWhiteSpace(_user.Id, nameof(_user.Id));

                    // Check if user is authorized with policy
                    if (!await _identityService.AuthorizeAsync(_user.Id, policy))
                    {
                        _logger.LogWarning("User is not authorized for policy-based access with policy: {Policy}.", policy);
                        throw PermissionException.Denied($"policy-based authorization with policy: {policy}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authorizing user with policies.");
                throw new AuthorizationException("An error occurred while authorizing user with policies.", "An error occurred while authorizing user with policies.", ex);
            }
        }
    }
}
