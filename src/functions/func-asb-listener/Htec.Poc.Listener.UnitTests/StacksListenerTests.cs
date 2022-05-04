using System;
using System.Text;
using Amido.Stacks.Core.Operations;
using Amido.Stacks.Messaging.Azure.ServiceBus.Extensions;
using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using Htec.Poc.Application.CQRS.Events;
using Amido.Stacks.Application.CQRS.ApplicationEvents;

namespace Htec.Poc.Listener.UnitTests;

[Trait("TestType", "UnitTests")]
public class StacksListenerTests
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;
    private readonly IApplicationEventHandler<RewardCalculatedEvent> applicationEventHandler;

    public StacksListenerTests()
    {
        msgReader = Substitute.For<IMessageReader>();
        logger = Substitute.For<ILogger<StacksListener>>();
        applicationEventHandler = Substitute.For<IApplicationEventHandler<RewardCalculatedEvent>>();
    }

    [Fact]
    public void TestExecution()
    {
        var msgBody = BuildMessageBody();
        var message = BuildMessage(msgBody);

        var stacksListener = new StacksListener(msgReader, logger, applicationEventHandler);

        stacksListener.Run(message);

        msgReader.Received(1).Read<StacksCloudEvent<RewardCalculatedEvent>>(message);
    }

    public RewardCalculatedEvent BuildMessageBody()
    {
        var memberId = Guid.NewGuid();
        var points = 99;
        return new RewardCalculatedEvent(new TestOperationContext(), memberId, points);
    }

    public Message BuildMessage(RewardCalculatedEvent body)
    {
        Guid correlationId = GetCorrelationId(body);

        var convertedMessage = new Message
        {
            CorrelationId = $"{correlationId}",
            ContentType = "application/json;charset=utf-8",
            Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))
        };

        return convertedMessage
            .SetEnclosedMessageType(body.GetType())
            .SetSerializerType(GetType());
    }

    private static Guid GetCorrelationId(object body)
    {
        var ctx = body as IOperationContext;
        return ctx?.CorrelationId ?? Guid.NewGuid();
    }
}
