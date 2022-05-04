using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;
using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using System.Threading.Tasks;

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
        Connection = "SERVICEBUS_CONNECTIONSTRING")] Message mySbMsg)
    {
        var applicationEvent = msgReader.Read<StacksCloudEvent<RewardCalculatedEvent>>(mySbMsg);
        //logger.LogInformation($"Message read. CorrelationId: {applicationEvent?.Data?.CorrelationId}");
        //logger.LogInformation($"Message read. EventCode: {applicationEvent?.Data?.EventCode}");
        //logger.LogInformation($"Message read. MemberId: {applicationEvent?.Data?.MemberId}");
        //logger.LogInformation($"Message read. Points: {applicationEvent?.Data?.Points}");
        //logger.LogInformation($"C# ServiceBus topic trigger function processed message: {applicationEvent}");

        await applicationEventHandler.HandleAsync(new RewardCalculatedEvent(
            105,
            Guid.NewGuid(),
            Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
            99));
    }
}
