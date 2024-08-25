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

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool succeeded, T? value, IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
            : base(succeeded, errors, errorCode, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, Array.Empty<string>());
        }

        public static new Result<T> Failure(IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
        {
            return new Result<T>(false, default, errors, errorCode, errorMessage);
        }

        public static new Result<T> Failure(string error, int? errorCode = null, string? errorMessage = null)
        {
            return Failure(new[] { error }, errorCode, errorMessage);
        }

        public static new Result<T> NotFound(string entityName, string reason = "")
        {
            var result = Result.NotFound(entityName, reason);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> ValidationFailure(IEnumerable<string> errors)
        {
            var result = Result.ValidationFailure(errors);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> Unauthorized(string action = "perform this action")
        {
            var result = Result.Unauthorized(action);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> Conflict(string entityName, string reason)
        {
            var result = Result.Conflict(entityName, reason);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> RateLimited(string message = "Too many requests")
        {
            var result = Result.RateLimited(message);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> UnexpectedError(Exception ex)
        {
            var result = Result.UnexpectedError(ex);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> InvalidOperation(string operation, string reason)
        {
            var result = Result.InvalidOperation(operation, reason);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> ResourceLocked(string resourceName, string reason)
        {
            var result = Result.ResourceLocked(resourceName, reason);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new Result<T> PreconditionFailed(string condition)
        {
            var result = Result.PreconditionFailed(condition);
            return new Result<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static Result<T> InvalidArgument(string argumentName, string reason)
        {
            var errorMessage = $"Invalid argument: {argumentName}. Reason: {reason}";
            return new Result<T>(false, default, new[] { errorMessage }, 400);
        }
    }
}
