namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class CoreException : Exception
    {
        public string ErrorCode { get; }
        public string Details { get; }
        public int StatusCode { get; }

        // Constructor with default values for message and details
        protected CoreException(string message, string details, int statusCode, string errorCode, Exception? innerException = null)
            : base(message, innerException)
        {
            if (details == null) throw new ArgumentNullException(nameof(details), "Details cannot be null.");

            ErrorCode = errorCode;
            Details = details;
            StatusCode = statusCode;
        }
    }

    public class NullArgumentException : CoreException
    {
        public NullArgumentException(string argName, string errorCode = "NULL_ARGUMENT", Exception? innerException = null)
            : base($"{argName} cannot be null", $"{argName} cannot be null", 400, errorCode, innerException)
        {
        }
    }

    public class InvalidArgumentException : CoreException
    {
        public InvalidArgumentException(string argName, string errorCode = "INVALID_ARGUMENT", Exception? innerException = null)
            : base($"{argName} is invalid", $"{argName} is invalid", 400, errorCode, innerException)
        {
        }
    }

    public class NotFoundException : CoreException
    {
        public NotFoundException(string entityName, string errorCode = "NOT_FOUND", Exception? innerException = null)
            : base($"{entityName} was not found", $"{entityName} was not found", 404, errorCode, innerException)
        {
        }
    }

    public class OperationFailedException : CoreException
    {
        public OperationFailedException(string operation, string errorCode = "OPERATION_FAILED", Exception? innerException = null)
            : base($"Operation failed: {operation}", $"Operation failed: {operation}", 500, errorCode, innerException)
        {
        }
    }

    public class ActionUnauthorizedException : CoreException
    {
        public ActionUnauthorizedException(string action, string errorCode = "UNAUTHORIZED_ACCESS", Exception? innerException = null)
            : base($"Unauthorized access for action: {action}", $"Unauthorized access for action: {action}", 403, errorCode, innerException)
        {
        }
    }

    public static class CoreExceptionFactory
    {
        // Method to create a NullArgumentException
        public static CoreException CreateNullArgumentException(
            string argName,
            string errorCode = "NULL_ARGUMENT",
            Exception? innerException = null)
        {
            return new NullArgumentException(argName, errorCode, innerException);
        }

        // Method to create an InvalidArgumentException
        public static CoreException CreateInvalidArgumentException(
            string argName,
            string errorCode = "INVALID_ARGUMENT",
            Exception? innerException = null)
        {
            return new InvalidArgumentException(argName, errorCode, innerException);
        }

        // Method to create a NotFoundException
        public static CoreException CreateNotFoundException(
            string entityName,
            string errorCode = "NOT_FOUND",
            Exception? innerException = null)
        {
            return new NotFoundException(entityName, errorCode, innerException);
        }

        // Method to create an ActionUnauthorizedException
        public static CoreException CreateActionUnauthorizedException(
            string action,
            string errorCode = "UNAUTHORIZED_ACCESS",
            Exception? innerException = null)
        {
            return new ActionUnauthorizedException(action, errorCode, innerException);
        }

        // Method to create an OperationFailedException
        public static CoreException CreateOperationFailedException(
            string operation,
            string errorCode = "OPERATION_FAILED",
            Exception? innerException = null)
        {
            return new OperationFailedException(operation, errorCode, innerException);
        }
    }

    // Static class to hold error codes and their corresponding messages
    public static class ErrorCodeMessages
    {
        private static readonly Dictionary<string, string> _errorMessages = new()
        {
            { "NULL_ARGUMENT", "The argument cannot be null." },
            { "INVALID_ARGUMENT", "The argument provided is invalid." },
            { "NOT_FOUND", "The requested resource was not found." },
            { "OPERATION_FAILED", "The operation could not be completed." },
            { "UNAUTHORIZED_ACCESS", "You do not have permission to perform this action." },
            // Add more error codes and messages as needed
        };

        public static IReadOnlyDictionary<string, string> ErrorMessages => _errorMessages;
    }
}
