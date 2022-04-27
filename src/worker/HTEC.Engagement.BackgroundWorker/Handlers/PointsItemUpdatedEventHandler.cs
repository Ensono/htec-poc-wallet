using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsItemUpdatedEventHandler : IApplicationEventHandler<PointsItemUpdatedEvent>
	{
		private readonly ILogger<PointsItemUpdatedEventHandler> log;

		public PointsItemUpdatedEventHandler(ILogger<PointsItemUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsItemUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing PointsItemUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
