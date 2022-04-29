using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class DeleteWalletItemCommandHandler : WalletCommandHandlerBase<DeleteWalletItem, bool>
{
    public DeleteWalletItemCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, DeleteWalletItem command)
    {
        wallet.RemoveWalletItem(command.CategoryId, command.WalletItemId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, DeleteWalletItem command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            new CategoryUpdatedEvent(command, command.WalletId, command.CategoryId),
            new WalletItemDeletedEvent(command, command.WalletId, command.CategoryId, command.WalletItemId)
        };
    }
}
