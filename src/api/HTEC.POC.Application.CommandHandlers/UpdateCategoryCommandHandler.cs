using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class UpdateCategoryCommandHandler : WalletCommandHandlerBase<UpdateCategory, bool>
{
    public UpdateCategoryCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, UpdateCategory command)
    {
        wallet.UpdateCategory(command.CategoryId, command.Name, command.Description);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, UpdateCategory command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            new CategoryUpdatedEvent(command, command.WalletId, command.CategoryId)
        };
    }
}
