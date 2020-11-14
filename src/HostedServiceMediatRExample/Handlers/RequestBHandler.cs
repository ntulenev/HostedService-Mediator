using System;
using System.Threading;
using System.Threading.Tasks;

using HostedServiceMediatRExample.Models;

using MediatR;
using Microsoft.Extensions.Logging;

namespace HostedServiceMediatRExample.Handlers
{
    public class RequestBHandler : INotificationHandler<RequestB>, IDisposable
    {
        public RequestBHandler(ILogger<RequestAHandler>? logger)
        {
            _logger = logger;
        }

        public async Task Handle(RequestB notification, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            await Task.Delay(100);
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

        private ILogger<RequestAHandler>? _logger;
    }
}
