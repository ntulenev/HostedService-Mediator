using MediatR;

namespace HostedServiceMediatRExample.Models
{
    public abstract class Request : INotification
    {
        public RequestId Id { get; }

        public Request(RequestId id)
        {
            Id = id;
        }
    }
}
