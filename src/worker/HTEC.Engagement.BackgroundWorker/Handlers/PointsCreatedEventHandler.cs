using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsCreatedEventHandler : IApplicationEventHandler<PointsCreatedEvent>
	{
		private readonly ILogger<PointsCreatedEventHandler> log;

		public PointsCreatedEventHandler(ILogger<PointsCreatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsCreatedEvent appEvent)
		{
			log.LogInformation($"Executing PointsCreatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
