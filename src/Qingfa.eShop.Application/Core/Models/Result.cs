namespace QingFa.EShop.Application.Core.Models
{
    public class Result
    {
        public bool Succeeded { get; }
        public string[] Errors { get; }
        public int? ErrorCode { get; }
        public string? ErrorMessage { get; }

        protected Result(bool succeeded, IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
        {
            return new Result(false, errors, errorCode, errorMessage);
        }

        public static Result Failure(string error, int? errorCode = null, string? errorMessage = null)
        {
            return Failure(new[] { error }, errorCode, errorMessage);
        }

        public static Result NotFound(string entityName, string reason = "")
        {
            string message = string.IsNullOrEmpty(reason)
                ? $"{entityName} not found."
                : $"{entityName} not found. Reason: {reason}";

            return Failure(message, 404, "Not Found");
        }

        public static Result ValidationFailure(IEnumerable<string> errors)
        {
            return Failure(errors, 400, "Validation Failed");
        }

        public static Result Unauthorized(string action = "perform this action")
        {
            return Failure($"You are not authorized to {action}.", 401, "Unauthorized");
        }

        public static Result Conflict(string entityName, string reason)
        {
            return Failure($"{entityName} conflict: {reason}", 409, "Conflict");
        }

        public static Result RateLimited(string message = "Too many requests")
        {
            return Failure(message, 429, "Too Many Requests");
        }

        public static Result UnexpectedError(Exception ex)
        {
            return Failure(ex.Message, 500, "Internal Server Error");
        }

        public static Result InvalidOperation(string operation, string reason)
        {
            return Failure($"{operation} failed due to: {reason}", 422, "Unprocessable Entity");
        }

        public static Result ResourceLocked(string resourceName, string reason)
        {
            return Failure($"{resourceName} is currently locked: {reason}", 423, "Locked");
        }

        public static Result PreconditionFailed(string condition)
        {
            return Failure($"Precondition failed: {condition}", 412, "Precondition Failed");
        }
    }
}
