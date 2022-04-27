using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class WalletUpdatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public WalletUpdatedEvent(int operationCode, Guid correlationId, Guid walletId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        WalletId = walletId;
    }

    public WalletUpdatedEvent(IOperationContext context, Guid walletId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        WalletId = walletId;
    }

    public int EventCode => (int)Enums.EventCode.WalletUpdated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid WalletId { get; }
}
