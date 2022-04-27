using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

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
