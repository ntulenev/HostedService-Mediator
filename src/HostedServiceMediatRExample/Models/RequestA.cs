namespace HostedServiceMediatRExample.Models;

public record RequestA : Request
{
    public RequestA(RequestId id) : base(id)
    {

    }
}
