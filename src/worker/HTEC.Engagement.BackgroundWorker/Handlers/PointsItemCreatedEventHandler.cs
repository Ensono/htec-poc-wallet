using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsItemCreatedEventHandler : IApplicationEventHandler<PointsItemCreatedEvent>
	{
		private readonly ILogger<PointsItemCreatedEventHandler> log;

		public PointsItemCreatedEventHandler(ILogger<PointsItemCreatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsItemCreatedEvent appEvent)
		{
			log.LogInformation($"Executing PointsItemCreatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
