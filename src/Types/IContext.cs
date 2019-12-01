using System;

namespace Bijector.Infrastructure.Types
{
    public interface IContext
    {
        Guid Id { get; }

        Guid UserId { get; }        

        string ResourceFrom { get; }

        string ResourceTo { get; }

        DateTimeOffset CreatedTime { get; }
    }
}