using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Stacks.Messaging.Azure.EventHub.Serializers;
using Microsoft.Azure.EventHubs;
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
    public void Run([EventHubTrigger(
        "%EVENTHUB_NAME%",
        Connection = "EVENTHUB_CONNECTIONSTRING")] EventData[] events)
    {
        var exceptions = new List<Exception>();

        foreach (EventData eventData in events)
        {
            try
            {
                var appEvent = msgReader.Read<WalletCreatedEvent>(eventData);
                logger.LogInformation($"Message read. Wallet Id: {appEvent?.WalletId}");
                logger.LogInformation($"C# Event Hub trigger function processed an event: {appEvent}");
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        if (exceptions.Count > 1)
            throw new AggregateException(exceptions);

        if (exceptions.Count == 1)
            throw exceptions.Single();
    }
}
