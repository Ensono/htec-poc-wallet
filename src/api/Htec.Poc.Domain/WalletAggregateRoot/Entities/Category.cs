using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Stacks.Domain;
using Newtonsoft.Json;
using Htec.Poc.Domain.WalletAggregateRoot.Exceptions;

namespace Htec.Poc.Domain.Entities;

public class Category : IEntity<Guid>
{
    public Category(Guid id, string name, string description, List<WalletItem> items = null)
    {
        Id = id;
        Name = name;
        Description = description;
        this.items = items ?? new List<WalletItem>();
    }

    [JsonProperty("Items")]
    private List<WalletItem> items;


    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public IReadOnlyList<WalletItem> Items { get => items?.AsReadOnly(); }


    internal void Update(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    internal void AddWalletItem(WalletItem item)
    {
        if (item.Price == 0)
            WalletItemPriceMustNotBeZeroException.Raise(item.Name);

        if (items.Any(i => i.Name == Name))
            WalletItemAlreadyExistsException.Raise(Id, item.Name);

        items.Add(item);
    }

    internal void RemoveWalletItem(Guid walletItemId)
    {
        var item = GetWalletItem(walletItemId);
        items.Remove(item);
    }

    internal void UpdateWalletItem(WalletItem walletItem)
    {
        var item = GetWalletItem(walletItem.Id);

        item.Name = walletItem.Name;
        item.Description = walletItem.Description;
        item.Price = walletItem.Price;
        item.Available = walletItem.Available;
    }

    private WalletItem GetWalletItem(Guid walletItemId)
    {
        var item = items.SingleOrDefault(i => i.Id == walletItemId);

        if (item == null)
            WalletItemDoesNotExistException.Raise(Id, walletItemId);

        return item;
    }
}
