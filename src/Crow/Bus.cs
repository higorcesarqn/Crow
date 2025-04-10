using Crow.Contracts;

namespace Crow;

public sealed class Bus(IServiceProvider serviceProvider) : IBus
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType()}");
        var method = handlerType.GetMethod("Process") ?? throw new InvalidOperationException($"Handler {handlerType} does not implement HandleAsync method.");
        return (Task<TResponse>)method.Invoke(handler, [request, cancellationToken])!;
    }

    public Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        var handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType()}");
        var method = handlerType.GetMethod("Process") ?? throw new InvalidOperationException($"Handler {handlerType} does not implement HandleAsync method.");
        return (Task)method.Invoke(handler, [request, cancellationToken])!;
    }

    public IAsyncEnumerable<TResponse> Send<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IStreamRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"No handler registered for stream request type {request.GetType()}");
        var method = handlerType.GetMethod("Process") ?? throw new InvalidOperationException($"Handler {handlerType} does not implement HandleAsync method.");
        return (IAsyncEnumerable<TResponse>)method.Invoke(handler, [request, cancellationToken])!;
    }
}
