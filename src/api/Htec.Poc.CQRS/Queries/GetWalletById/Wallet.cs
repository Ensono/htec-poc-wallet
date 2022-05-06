using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Htec.Poc.CQRS.Queries.GetWalletById;

public class Wallet
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public bool? Enabled { get; set; }

    [Required]
    public int Points { get; set; }

    [Required]
    public Guid MemberId { get; set; }

    [Required]
    public List<WalletHistory> History { get; set; }

    public static Wallet FromDomain(Domain.Wallet wallet)
    {
        return new Wallet()
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Enabled = wallet.Enabled,
            Points = wallet.Points,
            MemberId = wallet.MemberId,
            History = wallet.History?.Select(WalletHistory.FromEntity).ToList()
        };
    }
}
