using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.Common.Exceptions;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.Application.CommandHandlers;

public class DeleteWalletCommandHandler : ICommandHandler<DeleteWallet, bool>
{
    readonly IWalletRepository repository;
    readonly IApplicationEventPublisher applicationEventPublisher;

    public DeleteWalletCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<bool> HandleAsync(DeleteWallet command)
    {
        var wallet = await repository.GetByIdAsync(command.WalletId);

        if (wallet == null)
            WalletDoesNotExistException.Raise(command, command.WalletId);

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != wallet.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }

        var successful = await repository.DeleteAsync(command.WalletId);

        if (!successful)
            OperationFailedException.Raise(command, command.WalletId, "Unable to delete wallet");

        await applicationEventPublisher.PublishAsync(
            new WalletDeletedEvent(command, command.WalletId)
        );

        return successful;
    }
}
