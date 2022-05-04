using System;
using System.ComponentModel.DataAnnotations;

namespace Htec.Poc.API.Models.Requests;

/// <summary>
/// Request model used by CreateWallet api endpoint
/// </summary>
public class CreateWalletRequest
{
    /// <example>Name of wallet created</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Represents the status of the wallet. False if disabled
    /// </summary>
    [Required]
    public bool Enabled { get; set; }

    /// <summary>
    /// Represents the points balance the wallet.
    /// </summary>
    [Required]
    public int Points { get; set; }
}
