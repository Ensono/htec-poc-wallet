using Amido.Stacks.Domain;
using System;

namespace HTEC.Engagement.Listener.Repository
{
    public class Wallet : AggregateRoot<Guid>
    {
        public Wallet(Guid id, string name, string description, bool enabled, int balance)
        {
            Id = id;
            Name = name;
            Description = description;
            Enabled = enabled;
            Balance = balance;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Balance { get; private set; }

        public bool Enabled { get; private set; }

        public void Update(string name, string description, bool enabled, int balance)
        {
            this.Name = name;
            this.Description = description;
            this.Enabled = enabled;
            this.Balance = balance;
        }
    }
}