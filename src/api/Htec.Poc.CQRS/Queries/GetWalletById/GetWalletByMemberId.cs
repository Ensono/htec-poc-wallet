using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace Htec.Poc.CQRS.Queries.GetWalletById;

public class GetWalletByMemberId : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.GetWalletByMemberId;

    public Guid CorrelationId { get; }

    public Guid MemberId { get; set; }
}