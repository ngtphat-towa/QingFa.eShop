using QingFa.EShop.Domain.DomainModels;

namespace QingFa.eShop.Domain.Tests.DomainModels;

public class CoreExceptionTests
{
    [Fact]
    public void Exception_ShouldCreateException_WithGivenMessage()
    {
        var message = "Test message";
        var exception = CoreException.Exception(message);

        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void NullArgument_ShouldCreateException_WithArgumentName()
    {
        var arg = "TestArg";
        var exception = CoreException.NullArgument(arg);

        Assert.Equal($"{arg} cannot be null", exception.Message);
    }

    [Fact]
    public void InvalidArgument_ShouldCreateException_WithArgumentName()
    {
        var arg = "TestArg";
        var exception = CoreException.InvalidArgument(arg);

        Assert.Equal($"{arg} is invalid", exception.Message);
    }

    [Fact]
    public void NotFound_ShouldCreateException_WithArgumentName()
    {
        var arg = "TestArg";
        var exception = CoreException.NotFound(arg);

        Assert.Equal($"{arg} was not found", exception.Message);
    }
}