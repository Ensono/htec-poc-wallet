using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Data.Documents.Abstractions;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Htec.Poc.Common.Exceptions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Htec.Poc.Listener.Handlers;

public class UpdatePointsBalanceEventHandler : IApplicationEventHandler<RewardCalculatedEvent>
{
    private readonly IWalletRepository repository;
    private readonly ILogger<UpdatePointsBalanceEventHandler> logger;
    readonly IDocumentSearch<Domain.Wallet> storage;

    public UpdatePointsBalanceEventHandler(ILogger<UpdatePointsBalanceEventHandler> logger, IWalletRepository repository, IDocumentSearch<Domain.Wallet> storage)
    {
        this.logger = logger;
        this.repository = repository;
        this.storage = storage;
    }

    public async Task HandleAsync(RewardCalculatedEvent applicationEvent)
    {
        logger.LogInformation($"Executing UpdatePointsBalanceEventHandler...");

        var results = await storage.Search(filter => filter.MemberId == applicationEvent.MemberId);
        if (!results.IsSuccessful)
        {
            OperationFailedException.Raise(applicationEvent, applicationEvent.MemberId, "Unable to find wallet");
        }
           
        var wallet = results.Content.FirstOrDefault();
        if (wallet == null)
        {
            MemberDoesNotExistException.Raise(applicationEvent, applicationEvent.MemberId);
        }

        wallet.UpdatePointsBalance(applicationEvent.Points);

        var successful = await repository.SaveAsync(wallet);
        if (!successful)
        {
            OperationFailedException.Raise(applicationEvent, applicationEvent.MemberId, "Unable to update wallet");
        }
    }
}
