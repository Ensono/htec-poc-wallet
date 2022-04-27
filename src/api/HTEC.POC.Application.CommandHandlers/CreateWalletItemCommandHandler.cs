using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateWalletItemCommandHandler : WalletCommandHandlerBase<CreateWalletItem, Guid>
{
    public CreateWalletItemCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Wallet wallet, CreateWalletItem command)
    {
        id = Guid.NewGuid();

        wallet.AddWalletItem(
            command.CategoryId,
            id,
            command.Name,
            command.Description,
            command.Price,
            command.Available
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, CreateWalletItem command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            new CategoryUpdatedEvent(command, command.WalletId, command.CategoryId),
            new WalletItemCreatedEvent(command, command.WalletId, command.CategoryId, id)
        };
    }
}
