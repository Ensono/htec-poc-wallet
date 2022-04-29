using System;
using System.Collections.Generic;
using System.Linq;
using Htec.Poc.CQRS.Queries.SearchWallet;

namespace Htec.Poc.API.Models.Responses;

/// <summary>
/// Response model used by SearchWallet api endpoint
/// </summary>
public class SearchWalletResponse
{
    /// <example>10</example>
    public int Size { get; private set; }

    /// <example>0</example>
    public int Offset { get; private set; }

    /// <summary>
    /// Contains the items returned from the search
    /// </summary>
    public List<SearchWalletResponseItem> Results { get; private set; }

    public static SearchWalletResponse FromWalletResultItem(SearchWalletResult results)
    {
        return new SearchWalletResponse()
        {
            Offset = (results?.PageNumber ?? 0) * (results?.PageSize ?? 0),
            Size = (results?.PageSize ?? 0),
            Results = results?.Results?.Select(SearchWalletResponseItem.FromSearchWalletResultItem).ToList()
        };
    }
}
