using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class WalletItemDeletedEventHandler : IApplicationEventHandler<WalletItemDeletedEvent>
{
    private readonly ILogger<WalletItemDeletedEventHandler> log;

    public WalletItemDeletedEventHandler(ILogger<WalletItemDeletedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletItemDeletedEvent appEvent)
    {
        log.LogInformation($"Executing WalletItemDeletedEventHandler...");
        return Task.CompletedTask;
    }
}
