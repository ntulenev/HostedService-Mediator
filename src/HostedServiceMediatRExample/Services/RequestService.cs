using System.Diagnostics;
using HostedServiceMediatRExample.DataConsumers;
using HostedServiceMediatRExample.Models;

using MediatR;

namespace HostedServiceMediatRExample.Services
{
    public class RequestService : IHostedService
    {
        public RequestService(ILogger<RequestService>? logger,
                              IDataConsumer<Request> consumer,
                              IHostApplicationLifetime hostApplicationLifetime,
                              IServiceScopeFactory scopeFactory)
        {
            _logger = logger;

            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));

            _hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));

            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            workTask = ProcessDataAsync(_hostApplicationLifetime.ApplicationStopping);
            _ = workTask.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    _logger?.LogError("Encountered error.", t.Exception);

                    _logger?.LogInformation("Stopping the application.");

                    _hostApplicationLifetime.StopApplication();
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                Debug.Assert(workTask != null);
                await workTask.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }

            _logger?.LogInformation("The service is stopped.");
        }

        private async Task ProcessDataAsync(CancellationToken ct)
        {
            try
            {
                await foreach (var request in _consumer.GetDataAsync().WithCancellation(ct).ConfigureAwait(false))
                {
                    Debug.Assert(request != null);

                    // Using scope to have Mediator and handlers lifetime only per scope.
                    using var scope = _scopeFactory.CreateScope();

                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    Debug.Assert(mediator != null);

                    _logger?.LogInformation("Consume {@request}.", request);

                    await mediator.Publish(request, ct).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Error on data consuming... {ex}", ex);
                _hostApplicationLifetime.StopApplication();
            }
        }

        private Task? workTask;

        private readonly ILogger<RequestService>? _logger;

        private readonly IDataConsumer<Request> _consumer;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private readonly IServiceScopeFactory _scopeFactory;
    }
}
