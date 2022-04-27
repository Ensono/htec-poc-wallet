using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events;

public class WalletItemCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public WalletItemCreatedEvent(int operationCode, Guid correlationId, Guid walletId, Guid categoryId, Guid walletItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
    }

    public WalletItemCreatedEvent(IOperationContext context, Guid walletId, Guid categoryId, Guid walletItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
    }

    public int EventCode => (int)Enums.EventCode.WalletItemCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid WalletItemId { get; set; }
}
