using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;
using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Azure.Messaging.ServiceBus;

namespace Htec.Poc.Listener;

public class StacksListener
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;
    private readonly IApplicationEventHandler<RewardCalculatedEvent> applicationEventHandler;

    public StacksListener(
        IMessageReader msgReader, 
        ILogger<StacksListener> logger,
        IApplicationEventHandler<RewardCalculatedEvent> applicationEventHandler)
    {
        this.msgReader = msgReader;
        this.logger = logger;
        this.applicationEventHandler = applicationEventHandler ?? throw new ArgumentNullException(nameof(applicationEventHandler));
    }

    [FunctionName("StacksListener")]
    public async Task Run([ServiceBusTrigger(
        "%TOPIC_NAME%",
        "%SUBSCRIPTION_NAME%",
        Connection = "SERVICEBUS_CONNECTIONSTRING")] ServiceBusReceivedMessage message)
    {
        var applicationEvent = JsonConvert.DeserializeObject<StacksCloudEvent<RewardCalculatedEvent>>(Encoding.UTF8.GetString(message.Body));

        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {applicationEvent}");

        await applicationEventHandler.HandleAsync(applicationEvent?.Data);
    }
}
