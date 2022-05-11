using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

public class UpdateWalletCommandHandler : WalletCommandHandlerBase<UpdateWallet, bool>
{
    public UpdateWalletCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, UpdateWallet command)
    {
        wallet.Update(command.Name, command.Enabled, command.Points);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, UpdateWallet command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId)
        };
    }
}
