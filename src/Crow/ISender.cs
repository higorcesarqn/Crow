using Crow.Contracts;

namespace Crow;

public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> response, CancellationToken cancellationToken = default);

    Task Send(IRequest response, CancellationToken cancellationToken = default);

    IAsyncEnumerable<TResponse> Send<TResponse>(IStreamRequest<TResponse> response, CancellationToken cancellationToken = default);
}
