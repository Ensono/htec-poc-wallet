using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Query = HTEC.POC.CQRS.Queries.GetWalletById;

namespace HTEC.POC.API.Models.Responses;

/// <summary>
/// Response model used by GetById api endpoint
/// </summary>
public class Wallet
{
    /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
    [Required]
    public Guid? Id { get; private set; }

    /// <example>Wallet name</example>
    [Required]
    public string Name { get; private set; }

    /// <example>Wallet description</example>
    public string Description { get; private set; }

    /// <summary>
    /// Represents the categories contained in the wallet
    /// </summary>
    public List<Category> Categories { get; private set; }

    /// <summary>
    /// Represents the status of the wallet. False if disabled
    /// </summary>
    [Required]
    public bool? Enabled { get; private set; }

    public static Wallet FromQuery(Query.Wallet wallet)
    {
        return new Wallet
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Description = wallet.Description,
            Categories = wallet.Categories?.Select(Category.FromQuery).ToList(),
            Enabled = wallet.Enabled
        };
    }
}
