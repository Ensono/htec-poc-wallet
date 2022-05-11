using Amido.Stacks.Domain;
using System;

namespace Htec.Poc.Domain.Entities
{
    public class WalletHistory : IEntity<Guid>
    {
        public WalletHistory(Guid id, int points, DateTime changedAtUtc)
        {
            Id = id;
            Points = points;
            ChangedAtUtc = changedAtUtc;
        }

        public Guid Id { get; private set; }

        public int Points { get; private set; }

        public DateTime ChangedAtUtc { get; private set; }
    }
}
