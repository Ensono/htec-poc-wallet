using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Commands;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class IssuePointsCommandHandler : PointsCommandHandlerBase<IssuePoints, bool>
    {
        public IssuePointsCommandHandler(IPointsRepository repository, IApplicationEventPublisher applicationEventPublisher) : base(repository, applicationEventPublisher)
        {
        }

        public override Task<bool> HandleCommandAsync(Points points, IssuePoints command)
        {
            points.Issue(command.Points);

            return Task.FromResult(true);
        }

        public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Points points, IssuePoints command)
        {
            return new IApplicationEvent[] {
                new PointsIssuedEvent(command, command.PointsId)
            };
        }
    }
}
