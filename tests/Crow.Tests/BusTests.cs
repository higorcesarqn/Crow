using Crow.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Crow.Tests;

public sealed class BusTests
{
    [Fact]
    public async Task Send_WithResponse_CallsHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        var mockHandler = new Mock<IRequestHandler<TestRequest, TestResponse>>();
        
        mockHandler
            .Setup(h => h.Process(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TestResponse { Result = "Test Result" });
            
        services.AddSingleton(mockHandler.Object);
        var provider = services.BuildServiceProvider();
        
        var bus = new Bus(provider);
        var request = new TestRequest { Value = "Test" };
        
        // Act
        var result = await bus.Send(request);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Result", result.Result);
        mockHandler.Verify(h => h.Process(request, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Send_WithoutResponse_CallsHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        var mockHandler = new Mock<IRequestHandler<TestCommand>>();
        
        mockHandler
            .Setup(h => h.Process(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            
        services.AddSingleton(mockHandler.Object);
        var provider = services.BuildServiceProvider();
        
        var bus = new Bus(provider);
        var command = new TestCommand { Value = "Test" };
        
        // Act
        await bus.Send(command);
        
        // Assert
        mockHandler.Verify(h => h.Process(command, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Send_StreamRequest_ReturnsStreamFromHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        var mockHandler = new Mock<IStreamRequestHandler<TestStreamRequest, string>>();
        
        var streamResults = new List<string> { "Result1", "Result2", "Result3" }.ToAsyncEnumerable();
        
        mockHandler
            .Setup(h => h.Process(It.IsAny<TestStreamRequest>(), It.IsAny<CancellationToken>()))
            .Returns(streamResults);
            
        services.AddSingleton(mockHandler.Object);
        var provider = services.BuildServiceProvider();
        
        var bus = new Bus(provider);
        var request = new TestStreamRequest { Value = "Test" };
        
        // Act
        var results = new List<string>();
        await foreach (var item in bus.Send(request))
        {
            results.Add(item);
        }
        
        // Assert
        Assert.Equal(3, results.Count);
        Assert.Contains("Result1", results);
        Assert.Contains("Result2", results);
        Assert.Contains("Result3", results);

        mockHandler.Verify(h => h.Process(request, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    // Test classes
    public class TestRequest : IRequest<TestResponse>
    {
        public string? Value { get; set; }
    }
    
    public class TestResponse
    {
        public string? Result { get; set; }
    }
    
    public class TestCommand : IRequest
    {
        public string? Value { get; set; }
    }
    
    public class TestStreamRequest : IStreamRequest<string>
    {
        public string? Value { get; set; }
    }
}
