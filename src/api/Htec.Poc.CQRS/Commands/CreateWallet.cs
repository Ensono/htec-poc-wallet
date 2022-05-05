using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace Htec.Poc.CQRS.Commands;

public class CreateWallet : ICommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateWallet;

    public Guid CorrelationId { get; }

    public string Name { get; set; }

    public bool Enabled { get; set; }

    public int Points { get; set; }

    public Guid MemberId { get; set; }

    public CreateWallet(Guid correlationId, string name, bool enabled, int points, Guid memberId)
    {
        CorrelationId = correlationId;
        Name = name;
        Enabled = enabled;
        Points = points;
        MemberId = memberId;
    }
}
