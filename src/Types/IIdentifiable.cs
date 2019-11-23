using System;

namespace Infrastructure.Types
{
    public interface IIdentifiable
    {
         Guid Id { get; }
    }
}