using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.Listener;

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
        var appEvent = msgReader.Read<StacksCloudEvent<WalletCreatedEvent>>(mySbMsg);

        // TODO: work with appEvent
        logger.LogInformation($"Message read. Wallet Id: {appEvent?.Data?.WalletId}");

        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
    }
}
