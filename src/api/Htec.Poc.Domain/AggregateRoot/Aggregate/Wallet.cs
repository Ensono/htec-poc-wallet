using System;
using Amido.Stacks.Domain;
using Htec.Poc.Domain.Events;

namespace Htec.Poc.Domain;

public class Wallet : AggregateRoot<Guid>
{
    public Wallet(Guid id, string name, Guid tenantId, string description, bool enabled)
    {
        Id = id;
        Name = name;
        TenantId = tenantId;
        Description = description;
        Enabled = enabled;
    }

    public string Name { get; private set; }

    public Guid TenantId { get; private set; }

    public string Description { get; private set; }

    public bool Enabled { get; private set; }

    public void Update(string name, string description, bool enabled)
    {
        this.Name = name;
        this.Description = description;
        this.Enabled = enabled;

        Emit(new WalletChanged());
    }
}

