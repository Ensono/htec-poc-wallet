using System;

namespace HTEC.POC.CQRS.Commands;

public class DeleteWallet : IWalletCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteWallet;

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public DeleteWallet(Guid walletId)
    {
        this.WalletId = walletId;
    }
}
