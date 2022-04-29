using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateWallet : IWalletCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateWallet;

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Enabled { get; set; }
}
