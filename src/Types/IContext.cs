using System;

namespace Bijector.Infrastructure.Types
{
    public interface IContext
    {
        int Id { get; }

        int UserId { get; }        

        string ResourceFrom { get; }

        string ResourceTo { get; }

        DateTimeOffset CreatedTime { get; }
    }
}