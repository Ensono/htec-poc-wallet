using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class WalletItemCreatedEventHandler : IApplicationEventHandler<WalletItemCreatedEvent>
{
    private readonly ILogger<WalletItemCreatedEventHandler> log;

    public WalletItemCreatedEventHandler(ILogger<WalletItemCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletItemCreatedEvent appEvent)
    {
        log.LogInformation($"Executing WalletItemCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
