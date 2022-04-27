namespace Amido.Stacks.Messaging.Azure.ServiceBus.Configuration;

public class ServiceBusConfiguration
{
    public ServiceBusPublisherConfiguration Publisher { get; set; }

    public ServiceBusConsumerConfiguration Consumer { get; set; }
}
