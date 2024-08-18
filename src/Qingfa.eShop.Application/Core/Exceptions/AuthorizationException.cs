using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Core.Exceptions
{
    public class AuthorizationException : CoreException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class with a default message.
        /// </summary>
        public AuthorizationException()
            : base("Authorization failed.", 403, "AUTHORIZATION_FAILED")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        public AuthorizationException(string message, string? details = null, Exception? innerException = null)
            : base(message, 403, "AUTHORIZATION_FAILED", details, innerException)
        {
        }
    }
}
