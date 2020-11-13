using System.Threading;
using System.Threading.Tasks;

using HostedServiceMediatRExample.Models;

using MediatR;
using Microsoft.Extensions.Logging;

namespace HostedServiceMediatRExample.Handlers
{
    public class RequestBHandler : INotificationHandler<RequestB>
    {
        public RequestBHandler(ILogger<RequestAHandler>? logger)
        {
            _logger = logger;
        }

        public async Task Handle(RequestB notification, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            _logger?.LogInformation(">>> Request B Handled - {@notification}", notification);

        }

        private ILogger<RequestAHandler>? _logger;
    }
}
