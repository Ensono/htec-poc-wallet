using System;

namespace HTEC.POC.CQRS.Commands;

public class CreateWalletItem : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateWalletItem;

    public Guid CorrelationId { get; set; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public CreateWalletItem(Guid correlationId, Guid walletId, Guid categoryId, string name, string description, double price, bool available)
    {
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        Available = available;
    }
}
