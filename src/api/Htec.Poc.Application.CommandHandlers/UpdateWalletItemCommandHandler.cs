using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class UpdateWalletItemCommandHandler : WalletCommandHandlerBase<UpdateWalletItem, bool>
{
    public UpdateWalletItemCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, UpdateWalletItem command)
    {
        wallet.UpdateWalletItem(
            command.CategoryId,
            command.WalletItemId,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, UpdateWalletItem command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            //new CategoryUpdated(command, command.WalletId, command.CategoryId),
            new WalletItemUpdatedEvent(command, command.WalletId, command.CategoryId, command.WalletItemId)
        };
    }
}
