namespace QingFa.EShop.Domain.DomainModels
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message)
        {
        }

        public static CoreException NullArgument(string argName)
        {
            return new CoreException($"{argName} cannot be null or empty.");
        }

        public static CoreException InvalidArgument(string argName)
        {
            return new CoreException($"{argName} is invalid.");
        }

        public static CoreException NotFound(string entityName)
        {
            return new CoreException($"{entityName} was not found.");
        }

        public static CoreException ArgumentOutOfRange(string argName, string message)
        {
            return new CoreException($"{argName} is out of range: {message}");
        }
    }
}
