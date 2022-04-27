using System;

namespace HTEC.POC.CQRS.Commands;

public class CreateCategory : IWalletCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.CreateCategory;

    public Guid CorrelationId { get; set; }

    public Guid WalletId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public CreateCategory(Guid correlationId, Guid walletId, string name, string description)
    {
        CorrelationId = correlationId;
        WalletId = walletId;
        Name = name;
        Description = description;
    }
}
