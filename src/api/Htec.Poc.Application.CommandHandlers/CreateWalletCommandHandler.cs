using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Commands;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.CommandHandlers;

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

        var newWallet = new Wallet(
            id: id,
            name: command.Name,
            enabled: command.Enabled,
            points: command.Points
        );

        await repository.SaveAsync(newWallet);

        await applicationEventPublisher.PublishAsync(new WalletCreatedEvent(command, id));

        return id;
    }
}
