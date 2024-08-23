namespace QingFa.EShop.Application.Core.Models
{
    public class ResultValue<T> : Result
    {
        public T? Value { get; }

        private ResultValue(bool succeeded, T? value, IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
            : base(succeeded, errors, errorCode, errorMessage)
        {
            Value = value;
        }

        public static ResultValue<T> Success(T value)
        {
            return new ResultValue<T>(true, value, Array.Empty<string>());
        }

        public static new ResultValue<T> Failure(IEnumerable<string> errors, int? errorCode = null, string? errorMessage = null)
        {
            return new ResultValue<T>(false, default, errors, errorCode, errorMessage);
        }

        public static new ResultValue<T> Failure(string error, int? errorCode = null, string? errorMessage = null)
        {
            return Failure(new[] { error }, errorCode, errorMessage);
        }

        public static new ResultValue<T> NotFound(string entityName, string reason = "")
        {
            var result = Result.NotFound(entityName, reason);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> ValidationFailure(IEnumerable<string> errors)
        {
            var result = Result.ValidationFailure(errors);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> Unauthorized(string action = "perform this action")
        {
            var result = Result.Unauthorized(action);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> Conflict(string entityName, string reason)
        {
            var result = Result.Conflict(entityName, reason);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> RateLimited(string message = "Too many requests")
        {
            var result = Result.RateLimited(message);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> UnexpectedError(Exception ex)
        {
            var result = Result.UnexpectedError(ex);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> InvalidOperation(string operation, string reason)
        {
            var result = Result.InvalidOperation(operation, reason);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> ResourceLocked(string resourceName, string reason)
        {
            var result = Result.ResourceLocked(resourceName, reason);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }

        public static new ResultValue<T> PreconditionFailed(string condition)
        {
            var result = Result.PreconditionFailed(condition);
            return new ResultValue<T>(false, default, result.Errors, result.ErrorCode, result.ErrorMessage);
        }
    }
}
