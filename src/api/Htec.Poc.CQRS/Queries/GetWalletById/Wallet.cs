using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Htec.Poc.CQRS.Queries.GetWalletById;

public class Wallet
{
    [Required]
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public bool? Enabled { get; set; }

    public static Wallet FromDomain(Domain.Wallet wallet)
    {
        return new Wallet()
        {
            Id = wallet.Id,
            TenantId = wallet.TenantId,
            Name = wallet.Name,
            Description = wallet.Description,
            Enabled = wallet.Enabled
        };
    }
}
