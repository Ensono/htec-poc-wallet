using System.Collections.Generic;

namespace Htec.Poc.CQRS.Queries.SearchWallet;

public class SearchWalletResult
{
    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }

    public IEnumerable<SearchWalletResultItem> Results { get; set; }
}
