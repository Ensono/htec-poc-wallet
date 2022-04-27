using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
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
}
