using System.Runtime.CompilerServices;

using HostedServiceMediatRExample.Models;

namespace HostedServiceMediatRExample.DataConsumers;

public class FakeRequestConsumer : IDataConsumer<Request>
{
    public IAsyncEnumerable<Request> GetDataAsync(
        CancellationToken ct = default)
        => GenerateRequestsAsync(ct);

    private static async IAsyncEnumerable<Request> GenerateRequestsAsync(
        [EnumeratorCancellation] CancellationToken ct)
    {
        var random = CreateRandom();
        long id = 0;

        while (true)
        {
            ct.ThrowIfCancellationRequested();

            await DelayNextAsync(ct).ConfigureAwait(false);

            var requestType = GetRandomRequestType(random);
            ValidateRequestType(requestType);

            var requestId = NextId(ref id);
            var request = CreateRequest(requestType, requestId);

            yield return request;
        }
    }

    private static Task DelayNextAsync(CancellationToken ct) =>
        Task.Delay(TimeSpan.FromSeconds(1), ct);

    private static Random CreateRandom() => new();

    private static RequestType GetRandomRequestType(Random random) =>
        (RequestType)random.Next(0, 2);

    private static void ValidateRequestType(RequestType requestType)
    {
        if (!Enum.IsDefined(requestType))
        {
            throw new InvalidOperationException($"Invalid request type {requestType}");
        }
    }

    private static RequestId NextId(ref long id) => new(++id);

    private static Request CreateRequest(RequestType requestType, RequestId id) =>
        requestType switch
        {
            RequestType.RequestA => new RequestA(id),
            RequestType.RequestB => new RequestB(id),
            _ => throw new InvalidOperationException($"Unknown model type {requestType}")
        };
}
