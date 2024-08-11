namespace QingFa.EShop.Domain.DomainModels.Exceptions
{
    /// <summary>
    /// Represents a core exception in the domain model, used for handling various error scenarios.
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CoreException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with the specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException Create(string message)
        {
            return new CoreException(message);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullArgument(string argumentName)
        {
            return new CoreException($"{argumentName} cannot be null.");
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null or empty argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null or empty.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullOrEmptyArgument(string argumentName)
        {
            return new CoreException($"{argumentName} cannot be null or empty.");
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating an invalid argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is invalid.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException InvalidArgument(string argumentName)
        {
            return new CoreException($"{argumentName} is invalid.");
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating that an entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that was not found.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NotFound(string entityName)
        {
            return new CoreException($"{entityName} was not found.");
        }
    }
}
