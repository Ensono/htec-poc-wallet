using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.POC.Application.CQRS.Events;

namespace HTEC.POC.BackgroundWorker.Handlers;

public class CategoryCreatedEventHandler : IApplicationEventHandler<CategoryCreatedEvent>
{
    private readonly ILogger<CategoryCreatedEventHandler> log;

    public CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(CategoryCreatedEvent appEvent)
    {
        log.LogInformation($"Executing CategoryCreatedEventHandler...");
        return Task.CompletedTask;
    }
}
