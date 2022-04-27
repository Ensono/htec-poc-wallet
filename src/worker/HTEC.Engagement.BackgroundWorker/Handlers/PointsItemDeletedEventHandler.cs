using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using HTEC.Engagement.Application.CQRS.Events;

namespace HTEC.Engagement.BackgroundWorker.Handlers
{
    public class PointsItemDeletedEventHandler : IApplicationEventHandler<PointsItemDeletedEvent>
	{
		private readonly ILogger<PointsItemDeletedEventHandler> log;

		public PointsItemDeletedEventHandler(ILogger<PointsItemDeletedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(PointsItemDeletedEvent appEvent)
		{
			log.LogInformation($"Executing PointsItemDeletedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
