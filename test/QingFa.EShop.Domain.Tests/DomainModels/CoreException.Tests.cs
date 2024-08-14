using FluentAssertions;

using QingFa.EShop.Domain.DomainModels;

namespace QingFa.eShop.Domain.Tests.DomainModels;

public class CoreExceptionTests
{
    [Fact]
    public void Exception_ShouldCreateException_WithGivenMessage()
    {
        // Arrange
        var message = "Test message";

        // Act
        var exception = CoreException.Exception(message);

        // Assert
        exception.Message.Should().Be(message);
    }

    [Fact]
    public void NullArgument_ShouldCreateException_WithArgumentName()
    {
        // Arrange
        var arg = "TestArg";

        // Act
        var exception = CoreException.NullArgument(arg);

        // Assert
        exception.Message.Should().Be($"{arg} cannot be null");
    }

    [Fact]
    public void InvalidArgument_ShouldCreateException_WithArgumentName()
    {
        // Arrange
        var arg = "TestArg";

        // Act
        var exception = CoreException.InvalidArgument(arg);

        // Assert
        exception.Message.Should().Be($"{arg} is invalid");
    }

    [Fact]
    public void NotFound_ShouldCreateException_WithArgumentName()
    {
        // Arrange
        var arg = "TestArg";

        // Act
        var exception = CoreException.NotFound(arg);

        // Assert
        exception.Message.Should().Be($"{arg} was not found");
    }
}
