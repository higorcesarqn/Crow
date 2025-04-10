using Crow.Contracts;

namespace Crow;

/// <summary>
/// Interface for handling requests that return a response
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the request</typeparam>
public interface IRequestHandler<in TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles a request and returns a response
    /// </summary>
    /// <param name="request">The request to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    Task<TResponse> Process(TRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a handler for a request with no response
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    /// <summary>
    /// Handles a request asynchronously
    /// </summary>
    /// <param name="request">The request to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task Process(TRequest request, CancellationToken cancellationToken = default);
}