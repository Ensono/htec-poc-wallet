using System;
using System.Collections.Generic;
using System.Text;
using Amido.Stacks.Messaging.Azure.EventHub.Serializers;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.Listener.UnitTests;

[Trait("TestType", "UnitTests")]
public class StacksListenerTests
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;

    public StacksListenerTests()
    {
        msgReader = Substitute.For<IMessageReader>();
        logger = Substitute.For<ILogger<StacksListener>>();
    }

    [Fact]
    public void TestExecution()
    {
        var eventData = new List<EventData>();

        var msgBody = BuildMessageBody();
        var message = BuildEventData(msgBody);

        eventData.Add(message);

        var stacksListener = new StacksListener(msgReader, logger);

        stacksListener.Run(eventData.ToArray());

        msgReader.Received(1).Read<WalletCreatedEvent>(message);
    }

    public WalletCreatedEvent BuildMessageBody()
    {
        var id = Guid.NewGuid();
        return new WalletCreatedEvent(new TestOperationContext(), id);
    }

    public EventData BuildEventData(WalletCreatedEvent body)
    {
        var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));

        var eventData = new EventData(byteArray);

        return eventData;
    }
}
