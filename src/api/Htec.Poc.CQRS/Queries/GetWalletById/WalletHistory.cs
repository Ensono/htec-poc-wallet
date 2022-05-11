using System;
using System.ComponentModel.DataAnnotations;
using Entity = Htec.Poc.Domain.Entities;

namespace Htec.Poc.CQRS.Queries.GetWalletById;

public class WalletHistory
{
    [Required]
    public Guid? Id { get; set; }

    [Required]
    public int Points { get; set; }

    [Required]
    public DateTime ChangedAtUtc { get; set; }

    public static WalletHistory FromEntity(Entity.WalletHistory i)
    {
        return new WalletHistory
        {
            Id = i.Id,
            Points = i.Points,
            ChangedAtUtc = i.ChangedAtUtc
        };
    }
}
