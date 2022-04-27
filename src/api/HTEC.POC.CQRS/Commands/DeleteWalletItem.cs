using System;

namespace HTEC.POC.CQRS.Commands;

public class DeleteWalletItem : IWalletItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.DeleteWalletItem;

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid WalletItemId { get; set; }

    public DeleteWalletItem(Guid correlationId, Guid walletId, Guid categoryId, Guid walletItemId)
    {
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
    }
}
