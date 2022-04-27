using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Amido.Stacks.Messaging.Azure.ServiceBus.Publisher;

public class EventPublisher : IApplicationEventPublisher
{
    private readonly ILogger<EventPublisher> _log;
    private readonly ServiceBusSender _serviceBusSender;

    public EventPublisher(ILogger<EventPublisher> log, ServiceBusSender serviceBusSender)
    {
        _log = log;
        _serviceBusSender = serviceBusSender;
    }

    public async Task PublishAsync(IApplicationEvent applicationEvent)
    {
        _log.LogInformation($"Publishing event {applicationEvent.CorrelationId}");

        var serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(applicationEvent));

        try
        {
            await _serviceBusSender.SendMessageAsync(serviceBusMessage);
            _log.LogInformation($"Event {applicationEvent.CorrelationId} has been published.");
        }
        catch (Exception exception)
        {
            _log.LogError($"Something went wrong. Exception thrown: {exception.Message}");
        }
    }
}