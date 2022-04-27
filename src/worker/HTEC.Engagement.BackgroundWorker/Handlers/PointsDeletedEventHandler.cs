using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsDeletedEventHandler : IApplicationEventHandler<PointsDeletedEvent>
	{
		private readonly ILogger<PointsDeletedEventHandler> log;

		public PointsDeletedEventHandler(ILogger<PointsDeletedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsDeletedEvent appEvent)
		{
			log.LogInformation($"Executing PointsDeletedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
