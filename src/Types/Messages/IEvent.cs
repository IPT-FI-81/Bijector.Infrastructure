namespace Infrastructure.Types.Messages
{
    public interface IEvent
    {
        
    }

    public interface IRejectedEvent
    {
        string Reason { get; }
    }
}