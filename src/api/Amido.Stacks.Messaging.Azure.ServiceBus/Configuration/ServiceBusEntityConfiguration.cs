using Amido.Stacks.Configuration;

namespace Amido.Stacks.Messaging.Azure.ServiceBus.Configuration;

public class ServiceBusEntityConfiguration
{
    /// <summary>
    /// Connection string for the Service Bus.
    /// </summary>
    public Secret ConnectionString { get; set; }

    public string QueueName { get; set; }
}
