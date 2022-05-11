using System;
using System.ComponentModel.DataAnnotations;
using Query = Htec.Poc.CQRS.Queries.GetWalletById;

namespace Htec.Poc.API.Models.Responses;

public class WalletHistory
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    [Required]
    public Guid? Id { get; private set; }

    /// <example>99</example>
    [Required]
    public int Points { get; private set; }

    [Required]
    public DateTime ChangedAtUtc { get; private set; }

    public static WalletHistory FromQuery(Query.WalletHistory item)
    {
        return new WalletHistory
        {
            Id = item.Id,
            Points = item.Points,
            ChangedAtUtc = item.ChangedAtUtc
        };
    }
}