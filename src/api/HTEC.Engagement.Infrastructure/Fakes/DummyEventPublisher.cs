using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;

namespace HTEC.Engagement.Infrastructure.Fakes
{
    public class DummyEventPublisher : IApplicationEventPublisher
    {
        readonly ILogger<DummyEventPublisher> logger;

        public DummyEventPublisher(ILogger<DummyEventPublisher> logger)
        {
            this.logger = logger;
        }

        public async Task PublishAsync(IApplicationEvent applicationEvent)
        {
            logger.LogInformation("Event published: {EventType}", applicationEvent.GetType());

            await Task.CompletedTask;
        }
    }
}