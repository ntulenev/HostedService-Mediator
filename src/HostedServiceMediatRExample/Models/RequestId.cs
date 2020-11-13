using System;

namespace HostedServiceMediatRExample.Models
{
    public struct RequestId : IEquatable<RequestId>
    {
        public long Id { get; }

        public RequestId(long id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is RequestId requestId)
            {
                return Equals(requestId);
            }

            return false;
        }
        public override int GetHashCode() => HashCode.Combine(Id);

        public bool Equals(RequestId other)
            => Id == other.Id;
    }
}