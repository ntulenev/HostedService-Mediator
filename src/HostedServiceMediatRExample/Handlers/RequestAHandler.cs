using HostedServiceMediatRExample.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceMediatRExample.Handlers
{
    public class RequestAHandler : INotificationHandler<RequestA>
    {
        public RequestAHandler(ILogger<RequestAHandler>? logger)
        {
            _logger = logger;
        }

        public async Task Handle(RequestA notification, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            _logger?.LogInformation(">>> Request A Handled - {@notification}", notification);

        }

        private ILogger<RequestAHandler>? _logger;
    }
}
