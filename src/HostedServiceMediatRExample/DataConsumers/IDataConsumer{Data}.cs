namespace HostedServiceMediatRExample.DataConsumers;

public interface IDataConsumer<TData>
{
    IAsyncEnumerable<TData> GetDataAsync(CancellationToken ct = default);
}
