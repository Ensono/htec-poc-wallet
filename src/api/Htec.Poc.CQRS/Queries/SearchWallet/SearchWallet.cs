using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace Htec.Poc.CQRS.Queries.SearchWallet;

public class SearchWallet : IQueryCriteria
{
    public int OperationCode => (int)Common.Operations.OperationCode.SearchWallet;

    public Guid CorrelationId { get; }

    public string SearchText { get; }

    public Guid? TenantId { get; }

    public int? PageSize { get; }

    public int? PageNumber { get; }

    public SearchWallet(Guid correlationId, string searchText, Guid? restaurantId, int? pageSize, int? pageNumber)
    {
        CorrelationId = correlationId;
        SearchText = searchText;
        TenantId = restaurantId;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
