using System;
using Newtonsoft.Json;

namespace Bijector.Infrastructure.Types
{
    
    public class BaseContext : IContext
    {
        public BaseContext(int id, int userId, string resourceFrom, string resourceTo)
        {
            Id = id;
            UserId = userId;
            ResourceFrom = resourceFrom;
            ResourceTo = resourceTo;
            CreatedTime = DateTimeOffset.UtcNow;
        }

        [JsonConstructor]
        public BaseContext(int id, int userId, string resourceFrom, string resourceTo, DateTimeOffset createdTime)
        {
            Id = id;
            UserId = userId;
            ResourceFrom = resourceFrom;
            ResourceTo = resourceTo;
            CreatedTime = createdTime;
        }

        public int Id { get; }

        public int UserId { get; }

        public string ResourceFrom { get; }

        public string ResourceTo { get; }

        public DateTimeOffset CreatedTime { get; }
    }
}