using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class WalletDeletedEvent : IApplicationEvent
{
    [JsonConstructor]
    public WalletDeletedEvent(int operationCode, Guid correlationId, Guid walletId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        WalletId = walletId;
    }

    public WalletDeletedEvent(IOperationContext context, Guid walletId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        WalletId = walletId;
    }

    public int EventCode => (int)Enums.EventCode.WalletDeleted;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }
}
