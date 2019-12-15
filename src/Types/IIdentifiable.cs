using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Bijector.Infrastructure.Types
{
    public interface IIdentifiable
    {        
         int Id { get; set; }
    }
}