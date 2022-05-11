using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Stacks.Domain;
using Htec.Poc.Domain.Entities;
using Htec.Poc.Domain.Events;
using Newtonsoft.Json;

namespace Htec.Poc.Domain;

public class Wallet : AggregateRoot<Guid>
{
    [JsonProperty("History")]
    private List<WalletHistory> walletHistory;


    public Wallet(Guid id, string name, bool enabled, int points, Guid memberId, List<WalletHistory> walletHistory = null)
    {
        Id = id;
        Name = name;
        Enabled = enabled;
        Points = points;
        MemberId = memberId;
        this.walletHistory = walletHistory ?? new List<WalletHistory>();
    }

    public string Name { get; private set; }

    public bool Enabled { get; private set; }

    public int Points { get; private set; }

    public Guid MemberId { get; private set; }

    [JsonIgnore]
    public IReadOnlyList<WalletHistory> History { get => walletHistory?.AsReadOnly(); private set => walletHistory = value.ToList(); }

    public void Update(string name, bool enabled, int points)
    {
        this.Name = name;
        this.Enabled = enabled;
        this.Points = points;

        Emit(new WalletChanged());
    }

    public void UpdatePointsBalance(int points)
    {
        this.Points += points;

        AddWalletHistoryItem(new WalletHistory(Guid.NewGuid(), points, DateTime.UtcNow));

        Emit(new WalletChanged());
    }

    public void AddWalletHistoryItem(WalletHistory walletHistoryItem)
    {
        walletHistory.Add(walletHistoryItem);
    }
}