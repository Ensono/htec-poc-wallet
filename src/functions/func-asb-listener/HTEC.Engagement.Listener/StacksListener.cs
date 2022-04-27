using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.Listener
{
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
            var appEvent = msgReader.Read<StacksCloudEvent<PointsCreatedEvent>>(mySbMsg);

            // TODO: work with appEvent
            logger.LogInformation($"Message read. Points Id: {appEvent?.Data?.PointsId}");

            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
        }
    }
}
