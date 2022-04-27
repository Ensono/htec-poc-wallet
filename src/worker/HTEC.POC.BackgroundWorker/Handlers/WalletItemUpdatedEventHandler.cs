using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class WalletItemUpdatedEventHandler : IApplicationEventHandler<WalletItemUpdatedEvent>
{
    private readonly ILogger<WalletItemUpdatedEventHandler> log;

    public WalletItemUpdatedEventHandler(ILogger<WalletItemUpdatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletItemUpdatedEvent appEvent)
    {
        log.LogInformation($"Executing WalletItemUpdatedEventHandler...");
        return Task.CompletedTask;
    }
}
