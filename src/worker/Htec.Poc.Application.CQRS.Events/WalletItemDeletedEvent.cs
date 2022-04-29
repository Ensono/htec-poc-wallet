using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class WalletItemDeletedEvent : IApplicationEvent
{
    [JsonConstructor]
    public WalletItemDeletedEvent(int operationCode, Guid correlationId, Guid walletId, Guid categoryId, Guid walletItemId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
    }

    public WalletItemDeletedEvent(IOperationContext context, Guid walletId, Guid categoryId, Guid walletItemId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
    }

    public int EventCode => (int)Enums.EventCode.WalletItemDeleted;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid WalletItemId { get; set; }
}
