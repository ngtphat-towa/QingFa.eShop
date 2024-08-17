using FluentAssertions;

using QingFa.EShop.Domain.DomainModels.Errors;

namespace QingFa.EShop.Domain.Tests.Cores
{
    public class CoreErrorsTests
    {
        [Theory]
        [InlineData("parameterName", "parameterName cannot be null.")]
        public void NullArgument_ShouldReturnExpectedError(string argName, string expectedMessage)
        {
            // Act
            var error = CoreErrors.NullArgument(argName);

            // Assert
            error.Code.Should().Be(ErrorCodes.NullArgument);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("parameterName", "parameterName is invalid.")]
        public void InvalidArgument_ShouldReturnExpectedError(string argName, string expectedMessage)
        {
            // Act
            var error = CoreErrors.InvalidArgument(argName);

            // Assert
            error.Code.Should().Be(ErrorCodes.InvalidArgument);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("EntityName", "EntityName was not found.")]
        public void NotFound_ShouldReturnExpectedError(string entityName, string expectedMessage)
        {
            // Act
            var error = CoreErrors.NotFound(entityName);

            // Assert
            error.Code.Should().Be(ErrorCodes.NotFound);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("operationName", "Operation failed: operationName")]
        public void OperationFailed_ShouldReturnExpectedError(string operation, string expectedMessage)
        {
            // Act
            var error = CoreErrors.OperationFailed(operation);

            // Assert
            error.Code.Should().Be(ErrorCodes.OperationFailed);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("actionName", "Unauthorized access for action: actionName")]
        public void UnauthorizedAccess_ShouldReturnExpectedError(string action, string expectedMessage)
        {
            // Act
            var error = CoreErrors.UnauthorizedAccess(action);

            // Assert
            error.Code.Should().Be(ErrorCodes.UnauthorizedAccess);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("entityName", "entityName already exists or is in conflict.")]
        public void Conflict_ShouldReturnExpectedError(string entity, string expectedMessage)
        {
            // Act
            var error = CoreErrors.Conflict(entity);

            // Assert
            error.Code.Should().Be(ErrorCodes.Conflict);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("userName", "Authentication failed for user: userName")]
        public void AuthenticationFailed_ShouldReturnExpectedError(string user, string expectedMessage)
        {
            // Act
            var error = CoreErrors.AuthenticationFailed(user);

            // Assert
            error.Code.Should().Be(ErrorCodes.AuthenticationFailed);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("resourceName", "Access to resource resourceName is forbidden.")]
        public void Forbidden_ShouldReturnExpectedError(string resource, string expectedMessage)
        {
            // Act
            var error = CoreErrors.Forbidden(resource);

            // Assert
            error.Code.Should().Be(ErrorCodes.Forbidden);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData("fieldName", "errorMessage", "fieldName: errorMessage")]
        public void ValidationError_ShouldReturnExpectedError(string fieldName, string message, string expectedMessage)
        {
            // Act
            var error = CoreErrors.ValidationError(fieldName, message);

            // Assert
            error.Code.Should().Be(ErrorCodes.ValidationError);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }

        [Fact]
        public void RateLimited_ShouldReturnExpectedError()
        {
            // Act
            var error = CoreErrors.RateLimited();

            // Assert
            error.Code.Should().Be(ErrorCodes.RateLimited);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be("Rate limit exceeded. Please try again later.");
        }

        [Theory]
        [InlineData("operationName", "Operation timed out: operationName")]
        public void Timeout_ShouldReturnExpectedError(string operation, string expectedMessage)
        {
            // Act
            var error = CoreErrors.Timeout(operation);

            // Assert
            error.Code.Should().Be(ErrorCodes.Timeout);
            // Adjust according to actual Error class properties or methods
            error.Description.Should().Be(expectedMessage);
        }
    }
}
