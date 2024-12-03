using HostedServiceMediatRExample.Models;

using MediatR;

namespace HostedServiceMediatRExample.Handlers;

/// <summary>
/// Handler for <see cref="RequestA"/>, registers automatically with MediatR
/// </summary>
public class RequestAHandler : INotificationHandler<RequestA>, IDisposable
{
    public RequestAHandler(ILogger<RequestAHandler>? logger) => _logger = logger;

    public async Task Handle(RequestA notification, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        _logger?.LogInformation(">>> Request A Handled - {@notification}", notification);

    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            _logger?.LogInformation("Dispose handler A");
        }
    }
    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
    }

    private bool _isDisposed;
    private readonly ILogger<RequestAHandler>? _logger;
}
