using HostedServiceMediatRExample.Models;

using MediatR;

namespace HostedServiceMediatRExample.Handlers;

/// <summary>
/// Handler for <see cref="RequestB"/>, registers automatically with MediatR
/// </summary>
public class RequestBHandler : INotificationHandler<RequestB>, IDisposable
{
    public RequestBHandler(ILogger<RequestAHandler>? logger)
    {
        _logger = logger;
    }

    public async Task Handle(RequestB notification, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        _logger?.LogInformation(">>> Request B Handled - {@notification}", notification);
    }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            _logger?.LogInformation("Dispose handler B");
        }
    }
    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, this);

    private bool _isDisposed;
    private readonly ILogger<RequestAHandler>? _logger;
}
