using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.POC.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a wallet
/// </summary>
public interface IWalletCommand : ICommand
{
    Guid WalletId { get; }
}
