using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events;

public class CategoryCreatedEvent : IApplicationEvent
{
    [JsonConstructor]
    public CategoryCreatedEvent(int operationCode, Guid correlationId, Guid walletId, Guid categoryId)
    {
        OperationCode = operationCode;
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
    }

    public CategoryCreatedEvent(IOperationContext context, Guid walletId, Guid categoryId)
    {
        OperationCode = context.OperationCode;
        CorrelationId = context.CorrelationId;
        WalletId = walletId;
        CategoryId = categoryId;
    }

    public int EventCode => (int)Enums.EventCode.CategoryCreated;

    public int OperationCode { get; }

    public Guid CorrelationId { get; }

    public Guid WalletId { get; }

    public Guid CategoryId { get; }
}
