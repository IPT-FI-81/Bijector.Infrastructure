using System;
using Newtonsoft.Json;

namespace Bijector.Infrastructure.Types
{
    
    public class BaseContext : IContext
    {
        public BaseContext(Guid id, Guid userId, string resourceFrom, string resourceTo)
        {
            Id = id;
            UserId = userId;
            ResourceFrom = resourceFrom;
            ResourceTo = resourceTo;
            CreatedTime = DateTimeOffset.UtcNow;
        }

        [JsonConstructor]
        public BaseContext(Guid id, Guid userId, string resourceFrom, string resourceTo, DateTimeOffset createdTime)
        {
            Id = id;
            UserId = userId;
            ResourceFrom = resourceFrom;
            ResourceTo = resourceTo;
            CreatedTime = createdTime;
        }

        public Guid Id { get; }

        public Guid UserId { get; }

        public string ResourceFrom { get; }

        public string ResourceTo { get; }

        public DateTimeOffset CreatedTime { get; }
    }
}