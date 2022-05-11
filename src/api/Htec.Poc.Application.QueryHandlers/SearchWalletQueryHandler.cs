using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Amido.Stacks.Data.Documents.Abstractions;
using Htec.Poc.CQRS.Queries.SearchWallet;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.QueryHandlers;

public class SearchWalletQueryHandler : IQueryHandler<SearchWallet, SearchWalletResult>
{
    readonly IDocumentSearch<Wallet> storage;

    public SearchWalletQueryHandler(IDocumentSearch<Wallet> storage)
    {
        this.storage = storage;
    }

    public async Task<SearchWalletResult> ExecuteAsync(SearchWallet criteria)
    {
        if (criteria == null)
            throw new ArgumentException("A valid SearchWalletQueryCriteria os required!");

        int pageSize = 10;
        int pageNumber = 1;
        var searchTerm = string.Empty;
        Guid tenantId = criteria.TenantId.HasValue ? criteria.TenantId.Value : Guid.Empty;

        if (criteria.PageSize.HasValue && criteria.PageSize > 0)
            pageSize = criteria.PageSize.Value;

        if (criteria.PageNumber.HasValue && criteria.PageNumber > 0)
            pageNumber = criteria.PageNumber.Value;

        if (!string.IsNullOrEmpty(criteria.SearchText))
            searchTerm = criteria.SearchText.Trim();

        bool restaurantIdProvided = criteria.TenantId.HasValue;

        var results = await storage.Search(
            itemFilter => string.IsNullOrEmpty(searchTerm) || itemFilter.Name.Contains(searchTerm),
            null,
            pageSize,
            pageNumber);

        var result = new SearchWalletResult();
        result.PageSize = pageSize;
        result.PageNumber = pageNumber;

        if (!results.IsSuccessful)
            return result;

        result.Results = results.Content.Select(i => new SearchWalletResultItem()
        {
            Id = i.Id,
            Name = i.Name,
            Enabled = i.Enabled
        });


        return result;
    }
}
