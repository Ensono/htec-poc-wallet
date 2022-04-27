using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class DeleteCategoryCommandHandler : WalletCommandHandlerBase<DeleteCategory, bool>
{
    public DeleteCategoryCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
        : base(repository, applicationEventPublisher)
    {
    }

    public override Task<bool> HandleCommandAsync(Wallet wallet, DeleteCategory command)
    {
        wallet.RemoveCategory(command.CategoryId);

        return Task.FromResult(true);
    }

    public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Wallet wallet, DeleteCategory command)
    {
        return new IApplicationEvent[] {
            new WalletUpdatedEvent(command, command.WalletId),
            new CategoryDeletedEvent(command, command.WalletId, command.CategoryId)
        };
    }
}
