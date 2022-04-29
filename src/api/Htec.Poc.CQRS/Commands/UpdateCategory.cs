using System;

namespace Htec.Poc.CQRS.Commands;

public class UpdateCategory : ICategoryCommand
{
    public int OperationCode => (int)Common.Operations.OperationCode.UpdateCategory;

    public Guid CorrelationId { get; }

    public Guid WalletId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public UpdateCategory(Guid correlationId, Guid walletId, Guid categoryId, string name, string description)
    {
        CorrelationId = correlationId;
        WalletId = walletId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }
}
