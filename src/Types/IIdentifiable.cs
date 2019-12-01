using System;

namespace Bijector.Infrastructure.Types
{
    public interface IIdentifiable
    {
         Guid Id { get; }
    }
}