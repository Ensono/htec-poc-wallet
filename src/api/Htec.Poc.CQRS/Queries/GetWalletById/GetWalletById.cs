using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace Htec.Poc.CQRS.Queries.GetWalletById;

public class GetWalletById : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.GetWalletById;

    public Guid CorrelationId { get; }

    public Guid Id { get; set; }
}
