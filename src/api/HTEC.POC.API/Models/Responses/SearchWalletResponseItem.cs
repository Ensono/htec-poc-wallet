using System;
using Query = HTEC.POC.CQRS.Queries.SearchWallet;

namespace HTEC.POC.API.Models.Responses;

/// <summary>
/// Response model representing a search result item in the SearchWallet api endpoint
/// </summary>
public class SearchWalletResponseItem
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    public Guid Id { get; private set; }

    /// <example>Wallet name</example>
    public string Name { get; private set; }

    /// <example>Wallet description</example>
    public string Description { get; private set; }

    /// <summary>
    /// Represents the status of the wallet. False if disabled
    /// </summary>
    public bool Enabled { get; private set; }

    public static SearchWalletResponseItem FromSearchWalletResultItem(Query.SearchWalletResultItem item)
    {
        return new SearchWalletResponseItem()
        {
            Id = item.Id ?? Guid.Empty,
            Name = item.Name,
            Description = item.Description,
            Enabled = item.Enabled ?? false
        };
    }
}
