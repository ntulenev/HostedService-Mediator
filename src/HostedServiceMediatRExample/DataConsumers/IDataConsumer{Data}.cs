namespace HostedServiceMediatRExample.DataConsumers;

public interface IDataConsumer<Data>
{
    public IAsyncEnumerable<Data> GetDataAsync(CancellationToken ct = default);
}
