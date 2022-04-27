using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateCategoryCommandHandler : WalletCommandHandlerBase<CreateCategory, Guid>
{
    public CreateCategoryCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    Guid id;
    public override Task<Guid> HandleCommandAsync(Wallet wallet, CreateCategory command)
    {
        id = Guid.NewGuid();

        wallet.AddCategory(
            id,
            command.Name,
            command.Description
        );

        return Task.FromResult(id);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, CreateCategory command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            new CategoryCreatedEvent(command, command.WalletId, id)
        };
    }
}
