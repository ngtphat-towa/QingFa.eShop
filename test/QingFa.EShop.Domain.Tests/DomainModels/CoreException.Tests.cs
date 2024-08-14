using FluentAssertions;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.eShop.Domain.Tests.DomainModels
{
    public class CoreExceptionTests
    {
        [Fact]
        public void NullArgumentException_ShouldSetProperties()
        {
            // Arrange
            var argName = "TestArgument";
            var errorCode = "NULL_ARGUMENT";
            var innerException = new System.Exception("Inner exception");

            // Act
            var exception = new NullArgumentException(argName, errorCode, innerException);

            // Assert
            exception.Message.Should().Be($"{argName} cannot be null");
            exception.Details.Should().Be($"{argName} cannot be null");
            exception.StatusCode.Should().Be(400);
            exception.ErrorCode.Should().Be(errorCode);
            exception.InnerException.Should().Be(innerException);
        }

        [Fact]
        public void InvalidArgumentException_ShouldSetProperties()
        {
            // Arrange
            var argName = "TestArgument";
            var errorCode = "INVALID_ARGUMENT";
            var innerException = new System.Exception("Inner exception");

            // Act
            var exception = new InvalidArgumentException(argName, errorCode, innerException);

            // Assert
            exception.Message.Should().Be($"{argName} is invalid");
            exception.Details.Should().Be($"{argName} is invalid");
            exception.StatusCode.Should().Be(400);
            exception.ErrorCode.Should().Be(errorCode);
            exception.InnerException.Should().Be(innerException);
        }

        [Fact]
        public void NotFoundException_ShouldSetProperties()
        {
            // Arrange
            var entityName = "TestEntity";
            var errorCode = "NOT_FOUND";
            var innerException = new System.Exception("Inner exception");

            // Act
            var exception = new NotFoundException(entityName, errorCode, innerException);

            // Assert
            exception.Message.Should().Be($"{entityName} was not found");
            exception.Details.Should().Be($"{entityName} was not found");
            exception.StatusCode.Should().Be(404);
            exception.ErrorCode.Should().Be(errorCode);
            exception.InnerException.Should().Be(innerException);
        }

        [Fact]
        public void OperationFailedException_ShouldSetProperties()
        {
            // Arrange
            var operation = "TestOperation";
            var errorCode = "OPERATION_FAILED";
            var innerException = new System.Exception("Inner exception");

            // Act
            var exception = new OperationFailedException(operation, errorCode, innerException);

            // Assert
            exception.Message.Should().Be($"Operation failed: {operation}");
            exception.Details.Should().Be($"Operation failed: {operation}");
            exception.StatusCode.Should().Be(500);
            exception.ErrorCode.Should().Be(errorCode);
            exception.InnerException.Should().Be(innerException);
        }

        [Fact]
        public void ActionUnauthorizedException_ShouldSetProperties()
        {
            // Arrange
            var action = "TestAction";
            var errorCode = "UNAUTHORIZED_ACCESS";
            var innerException = new System.Exception("Inner exception");

            // Act
            var exception = new ActionUnauthorizedException(action, errorCode, innerException);

            // Assert
            exception.Message.Should().Be($"Unauthorized access for action: {action}");
            exception.Details.Should().Be($"Unauthorized access for action: {action}");
            exception.StatusCode.Should().Be(403);
            exception.ErrorCode.Should().Be(errorCode);
            exception.InnerException.Should().Be(innerException);
        }
    }

    public class CoreExceptionFactoryTests
    {
        [Fact]
        public void CreateNullArgumentException_ShouldReturnExceptionWithDefaultValues()
        {
            // Arrange
            var argName = "TestArgument";

            // Act
            var exception = CoreExceptionFactory.CreateNullArgumentException(argName);

            // Assert
            exception.Should().BeOfType<NullArgumentException>();
            exception.Message.Should().Be($"{argName} cannot be null");
            exception.Details.Should().Be($"{argName} cannot be null");
            exception.StatusCode.Should().Be(400);
            exception.ErrorCode.Should().Be("NULL_ARGUMENT");
        }

        [Fact]
        public void CreateInvalidArgumentException_ShouldReturnExceptionWithDefaultValues()
        {
            // Arrange
            var argName = "TestArgument";

            // Act
            var exception = CoreExceptionFactory.CreateInvalidArgumentException(argName);

            // Assert
            exception.Should().BeOfType<InvalidArgumentException>();
            exception.Message.Should().Be($"{argName} is invalid");
            exception.Details.Should().Be($"{argName} is invalid");
            exception.StatusCode.Should().Be(400);
            exception.ErrorCode.Should().Be("INVALID_ARGUMENT");
        }

        [Fact]
        public void CreateNotFoundException_ShouldReturnExceptionWithDefaultValues()
        {
            // Arrange
            var entityName = "TestEntity";

            // Act
            var exception = CoreExceptionFactory.CreateNotFoundException(entityName);

            // Assert
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"{entityName} was not found");
            exception.Details.Should().Be($"{entityName} was not found");
            exception.StatusCode.Should().Be(404);
            exception.ErrorCode.Should().Be("NOT_FOUND");
        }

        [Fact]
        public void CreateOperationFailedException_ShouldReturnExceptionWithDefaultValues()
        {
            // Arrange
            var operation = "TestOperation";

            // Act
            var exception = CoreExceptionFactory.CreateOperationFailedException(operation);

            // Assert
            exception.Should().BeOfType<OperationFailedException>();
            exception.Message.Should().Be($"Operation failed: {operation}");
            exception.Details.Should().Be($"Operation failed: {operation}");
            exception.StatusCode.Should().Be(500);
            exception.ErrorCode.Should().Be("OPERATION_FAILED");
        }

        [Fact]
        public void CreateActionUnauthorizedException_ShouldReturnExceptionWithDefaultValues()
        {
            // Arrange
            var action = "TestAction";

            // Act
            var exception = CoreExceptionFactory.CreateActionUnauthorizedException(action);

            // Assert
            exception.Should().BeOfType<ActionUnauthorizedException>();
            exception.Message.Should().Be($"Unauthorized access for action: {action}");
            exception.Details.Should().Be($"Unauthorized access for action: {action}");
            exception.StatusCode.Should().Be(403);
            exception.ErrorCode.Should().Be("UNAUTHORIZED_ACCESS");
        }
    }
}
