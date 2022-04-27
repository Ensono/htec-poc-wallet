using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.POC.Application.CQRS.Events;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.CommandHandlers;

public class CreateWalletCommandHandler : ICommandHandler<CreateWallet, Guid>
{
    private readonly IWalletRepository repository;
    private readonly IApplicationEventPublisher applicationEventPublisher;

    public CreateWalletCommandHandler(IWalletRepository repository, IApplicationEventPublisher applicationEventPublisher)
    {
        this.repository = repository;
        this.applicationEventPublisher = applicationEventPublisher;
    }

    public async Task<Guid> HandleAsync(CreateWallet command)
    {
        var id = Guid.NewGuid();

        // TODO: Check if the user owns the resource before any operation
        // if(command.User.TenantId != wallet.TenantId)
        // {
        //     throw NotAuthorizedException()
        // }


        var newWallet = new Wallet(
            id: id,
            name: command.Name,
            tenantId: command.TenantId,
            description: command.Description,
            categories: null,
            enabled: command.Enabled
        );

        await repository.SaveAsync(newWallet);

        await applicationEventPublisher.PublishAsync(new WalletCreatedEvent(command, id));

        return id;
    }
}
