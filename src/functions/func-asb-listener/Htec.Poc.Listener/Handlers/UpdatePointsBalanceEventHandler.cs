using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Htec.Poc.Listener.Handlers
{
    public class UpdatePointsBalanceEventHandler : IApplicationEventHandler<RewardCalculatedEvent>
    {
        private readonly ILogger<UpdatePointsBalanceEventHandler> logger;

        public UpdatePointsBalanceEventHandler(ILogger<UpdatePointsBalanceEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(RewardCalculatedEvent applicationEvent)
        {
            logger.LogInformation($"Executing UpdatePointsBalanceEventHandler...");
            return Task.CompletedTask;
        }
    }
}
