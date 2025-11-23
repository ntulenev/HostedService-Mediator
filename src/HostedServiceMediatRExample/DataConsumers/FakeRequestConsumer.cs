using System.Runtime.CompilerServices;

using HostedServiceMediatRExample.Models;

namespace HostedServiceMediatRExample.DataConsumers;

public class FakeRequestConsumer : IDataConsumer<Request>
{
    public async IAsyncEnumerable<Request> GetDataAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        Random rnd = new();
        long id = 0;

        while (true)
        {
            ct.ThrowIfCancellationRequested();

            await Task.Delay(1_000, ct).ConfigureAwait(false);

            var enumItem = (RequestType)rnd.Next(0, 2);
            if (!Enum.IsDefined(enumItem))
            {
                throw new InvalidOperationException($"Invalid request type {enumItem}");
            }

            Request model = enumItem switch
            {
                RequestType.RequestA => new RequestA(new RequestId(++id)),
                RequestType.RequestB => new RequestB(new RequestId(++id)),
                _ => throw new InvalidOperationException("Unknown model type"),
            };

            yield return model;
        }
    }
}
