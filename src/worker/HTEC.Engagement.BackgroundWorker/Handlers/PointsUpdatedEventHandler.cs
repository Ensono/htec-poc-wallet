using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsUpdatedEventHandler : IApplicationEventHandler<PointsUpdatedEvent>
	{
		private readonly ILogger<PointsUpdatedEventHandler> log;

		public PointsUpdatedEventHandler(ILogger<PointsUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing PointsUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
