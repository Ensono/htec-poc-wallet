using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class UpdateWalletCommandHandler : WalletCommandHandlerBase<UpdateWallet, bool>
{
    public UpdateWalletCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, UpdateWallet command)
    {
        wallet.Update(command.Name, command.Description, command.Enabled);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, UpdateWallet command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId)
        };
    }
}
