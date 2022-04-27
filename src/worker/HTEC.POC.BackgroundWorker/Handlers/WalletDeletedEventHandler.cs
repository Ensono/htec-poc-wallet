using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class WalletDeletedEventHandler : IApplicationEventHandler<WalletDeletedEvent>
{
    private readonly ILogger<WalletDeletedEventHandler> log;

    public WalletDeletedEventHandler(ILogger<WalletDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletDeletedEvent appEvent)
    {
        log.LogInformation($"Executing WalletDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
