namespace HostedServiceMediatRExample.Models
{
    public record RequestB : Request
    {
        public RequestB(RequestId id) : base(id)
        {

        }
    }
}
