using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.Listener;

public class StacksListener
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;

    public StacksListener(IMessageReader msgReader, ILogger<StacksListener> logger)
    {
        this.msgReader = msgReader;
        this.logger = logger;
    }

    [FunctionName("StacksListener")]
    public void Run([ServiceBusTrigger(
        "%TOPIC_NAME%",
        "%SUBSCRIPTION_NAME%",
        Connection = "SERVICEBUS_CONNECTIONSTRING")] Message mySbMsg)
    {
        var appEvent = msgReader.Read<StacksCloudEvent<RewardCalculatedEvent>>(mySbMsg);

        // TODO: work with appEvent
        logger.LogInformation($"Message read. CorrelationId: {appEvent?.Data?.CorrelationId}");
        logger.LogInformation($"Message read. EventCode: {appEvent?.Data?.EventCode}");
        logger.LogInformation($"Message read. MemberId: {appEvent?.Data?.MemberId}");
        logger.LogInformation($"Message read. Points: {appEvent?.Data?.Points}");

        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
    }
}
