using Bijector.Infrastructure.Types;

namespace Bijector.Infrastructure.Queues
{
    public class RabbitMQMessage<TContent>
    {
        public RabbitMQMessage(TContent content, IContext context)
        {
            Content = content;
            Context = context;
        }

        public TContent Content { get; }

        public IContext Context { get; }
    }
}