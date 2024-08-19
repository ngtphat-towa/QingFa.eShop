namespace QingFa.EShop.Application.Core.Models
{
    public class Result
    {
        public bool Succeeded { get; }
        public string[] Errors { get; }

        private Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }

    public class ResultValue<T>
    {
        public bool Succeeded { get; }
        public string[] Errors { get; }
        public T? Value { get; }

        private ResultValue(bool succeeded, IEnumerable<string> errors, T? value = default)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Value = value;
        }

        public static ResultValue<T> Success(T value)
        {
            return new ResultValue<T>(true, Array.Empty<string>(), value);
        }

        public static ResultValue<T> Failure(IEnumerable<string> errors)
        {
            return new ResultValue<T>(false, errors);
        }
    }
}
