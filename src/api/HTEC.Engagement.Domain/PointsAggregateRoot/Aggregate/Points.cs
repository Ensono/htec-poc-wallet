using System;
using Amido.Stacks.Domain;
using HTEC.Engagement.Domain.Events;
using HTEC.Engagement.Domain.PointsAggregateRoot.Exceptions;

namespace HTEC.Engagement.Domain
{
    public class Points : AggregateRoot<Guid>
    {
        public Points(Guid id, string name, Guid tenantId, string description, bool enabled, int balance)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            Description = description;
            Enabled = enabled;
            Balance = balance;
        }

        public string Name { get; private set; }

        public Guid TenantId { get; private set; }

        public string Description { get; private set; }

        public int Balance { get; private set; }

        public bool Enabled { get; private set; }

        public void Update(string name, string description, bool enabled, int balance)
        {
            this.Name = name;
            this.Description = description;
            this.Enabled = enabled;
            this.Balance = balance;

            Emit(new PointsChanged());//TODO: Pass the event data
        }

        public void Redeem(int points)
        {
            if (points > Balance)
            {
                RedemptionGreaterThanBalanceException.Raise(points);
            }
            else
            {
                this.Balance = Balance - points;

                Emit(new PointsRedeemed());//TODO: Pass the event data
            }
        }

        public void Issue(int points)
        {
            this.Balance = Balance + points;

            Emit(new PointsIssued());//TODO: Pass the event data
        }
    }
}
