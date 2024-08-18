using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Application.Core.Exceptions
{
    public class PermissionException : CoreException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionException"/> class with a default message.
        /// </summary>
        public PermissionException()
            : base("Permission denied.", 403, "PERMISSION_DENIED")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        public PermissionException(string message, string? details = null, Exception? innerException = null)
            : base(message, 403, "PERMISSION_DENIED", details, innerException)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="PermissionException"/> with the specified message indicating insufficient permissions.
        /// </summary>
        /// <param name="resource">The resource for which permission was denied.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="PermissionException"/> instance.</returns>
        public static PermissionException Denied(string resource, string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException("Resource name cannot be null or empty.", nameof(resource));
            }

            return new PermissionException($"Access to the resource '{resource}' is denied.", details, innerException);
        }
    }
}
