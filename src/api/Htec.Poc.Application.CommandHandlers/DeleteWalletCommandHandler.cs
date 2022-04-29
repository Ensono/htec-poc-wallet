using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.Common.Exceptions;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.Application.CommandHandlers;

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
