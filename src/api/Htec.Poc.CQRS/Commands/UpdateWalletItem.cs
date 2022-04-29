using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateWalletItem : IWalletItemCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateWalletItem;

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid WalletItemId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public UpdateWalletItem(Guid correlationId, Guid walletId, Guid categoryId, Guid walletItemId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        WalletItemId = walletItemId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
