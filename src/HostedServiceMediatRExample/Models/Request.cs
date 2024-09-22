using MediatR;

namespace HostedServiceMediatRExample.Models;

public abstract record Request : INotification
{
    public RequestId Id { get; }

    public Request(RequestId id)
    {
        Id = id;
    }
}
