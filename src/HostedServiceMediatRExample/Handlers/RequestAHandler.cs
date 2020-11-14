using System;
using System.Threading;
using System.Threading.Tasks;

using HostedServiceMediatRExample.Models;

using MediatR;

using Microsoft.Extensions.Logging;

namespace HostedServiceMediatRExample.Handlers
{
    /// <summary>
    /// Handler for <see cref="RequestA"/>, registers automatically with MediatR
    /// </summary>
    public class RequestAHandler : INotificationHandler<RequestA>, IDisposable
    {
        public RequestAHandler(ILogger<RequestAHandler>? logger)
        {
            _logger = logger;
        }

        public async Task Handle(RequestA notification, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            await Task.Delay(100);
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
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private bool _isDisposed;

        private ILogger<RequestAHandler>? _logger;
    }
}
