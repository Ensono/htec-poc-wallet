using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Data.Documents.Abstractions;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Htec.Poc.Listener.Handlers
{
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
                logger.LogInformation($"Wallet not found in database.");
                throw new Exception("NOT FOUND!");
            }
               
            var wallet = results.Content.FirstOrDefault();

            wallet.UpdatePointsBalance(applicationEvent.Points);

            var successful = await repository.SaveAsync(wallet);
            if (!successful)
            {
                logger.LogInformation($"Wallet failed to save successfully.");
                throw new Exception("SAVE FAILED!");
            }
        }
    }
}
