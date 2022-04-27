using System;

namespace HTEC.POC.CQRS.Commands;

/// <summary>
/// Define required parameters for commands executed against a category
/// </summary>
public interface ICategoryCommand : IWalletCommand
{
    Guid CategoryId { get; }
}
