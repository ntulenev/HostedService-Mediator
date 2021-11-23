using HostedServiceMediatRExample.Models;

using MediatR;

namespace HostedServiceMediatRExample.Handlers
{
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

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _logger?.LogInformation("Dispose handler B");
            }
        }
        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private bool _isDisposed;

        private readonly ILogger<RequestAHandler>? _logger;
    }
}
