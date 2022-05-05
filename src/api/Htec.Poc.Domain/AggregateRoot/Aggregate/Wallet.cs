using System;
using Amido.Stacks.Domain;
using Htec.Poc.Domain.Events;

namespace Htec.Poc.Domain;

public class Wallet : AggregateRoot<Guid>
{
    public Wallet(Guid id, string name, bool enabled, int points, Guid memberId)
    {
        Id = id;
        Name = name;
        Enabled = enabled;
        Points = points;
        MemberId = memberId;
    }

    public string Name { get; private set; }

    public bool Enabled { get; private set; }

    public int Points { get; private set; }

    public Guid MemberId { get; private set; }

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

        Emit(new WalletChanged());
    }
}

