using System.Collections.Generic;
using System.Threading;

namespace HostedServiceMediatRExample.DataConsumers
{
    public interface IDataConsumer<Data>
    {
        public IAsyncEnumerable<Data> GetDataAsync(CancellationToken ct = default);
    }
}
