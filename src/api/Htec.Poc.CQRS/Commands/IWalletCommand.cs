using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace Htec.Poc.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a wallet
/// </summary>
public interface IWalletCommand : ICommand
{
    Guid WalletId { get; }
}
