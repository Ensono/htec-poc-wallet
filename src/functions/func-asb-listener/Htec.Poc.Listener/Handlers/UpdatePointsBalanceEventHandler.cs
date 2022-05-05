using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Htec.Poc.Application.Integration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Htec.Poc.Listener.Handlers
{
    public class UpdatePointsBalanceEventHandler : IApplicationEventHandler<RewardCalculatedEvent>
    {
        private readonly IWalletRepository repository;
        private readonly ILogger<UpdatePointsBalanceEventHandler> logger;

        public UpdatePointsBalanceEventHandler(ILogger<UpdatePointsBalanceEventHandler> logger, IWalletRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task HandleAsync(RewardCalculatedEvent applicationEvent)
        {
            logger.LogInformation($"Executing UpdatePointsBalanceEventHandler...");

            var wallet = await repository.GetByIdAsync(applicationEvent.MemberId);
            if (wallet == null)
            {
                logger.LogInformation($"Wallet not found in database.");
                throw new Exception("NOT FOUND!");
            }

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
