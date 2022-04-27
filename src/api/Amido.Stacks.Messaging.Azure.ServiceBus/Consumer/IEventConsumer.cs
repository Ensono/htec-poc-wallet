using System.Threading.Tasks;

namespace Amido.Stacks.Messaging.Azure.ServiceBus.Consumer;

public interface IEventConsumer
{
    Task ProcessAsync();
}