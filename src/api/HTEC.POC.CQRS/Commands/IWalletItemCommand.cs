using System;

namespace HTEC.POC.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a wallet item
/// </summary>
public interface IWalletItemCommand : ICategoryCommand
{
    Guid WalletItemId { get; }
}
