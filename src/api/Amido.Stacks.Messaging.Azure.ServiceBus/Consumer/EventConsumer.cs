using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Amido.Stacks.Messaging.Azure.ServiceBus.Consumer;

public class EventConsumer : IEventConsumer
{
    private readonly ILogger<EventConsumer> _log;
    private readonly ServiceBusReceiver _serviceBusReceiver;

    public EventConsumer(ILogger<EventConsumer> log, ServiceBusReceiver serviceBusReceiver)
    {
        _log = log;
        _serviceBusReceiver = serviceBusReceiver;
    }

    public async Task ProcessAsync()
    {
        var receivedMessage = await _serviceBusReceiver.ReceiveMessageAsync();

        string body = receivedMessage.Body.ToString();
        Console.WriteLine(body);
    }
}
