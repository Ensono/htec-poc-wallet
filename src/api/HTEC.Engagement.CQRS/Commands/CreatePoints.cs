using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.Engagement.CQRS.Commands
{
    public class CreatePoints : ICommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.CreatePoints;

        public Guid CorrelationId { get; }

        public Guid TenantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public int Balance { get; set; }

        public CreatePoints(Guid correlationId, Guid tenantId, string name, string description, bool enabled, int balance)
        {
            CorrelationId = correlationId;
            TenantId = tenantId;
            Name = name;
            Description = description;
            Enabled = enabled;
            Balance = balance;
        }
    }
}
