using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class WalletUpdatedEventHandler : IApplicationEventHandler<WalletUpdatedEvent>
{
    private readonly ILogger<WalletUpdatedEventHandler> log;

    public WalletUpdatedEventHandler(ILogger<WalletUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing WalletUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
