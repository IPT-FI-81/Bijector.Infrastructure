namespace Bijector.Infrastructure.Types.Messages
{
    public interface IEvent
    {
        
    }

    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
    }
}