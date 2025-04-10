using Crow.Contracts;

namespace Crow;

public interface IStreamRequestHandler<in TRequest, TResponse> 
    where TRequest : IStreamRequest<TResponse>
{
    IAsyncEnumerable<TResponse> Process(TRequest request, CancellationToken cancellationToken = default);
}
