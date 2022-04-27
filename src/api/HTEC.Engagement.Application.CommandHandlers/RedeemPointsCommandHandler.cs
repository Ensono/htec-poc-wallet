using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Commands;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class RedeemPointsCommandHandler : PointsCommandHandlerBase<RedeemPoints, bool>
    {
        public RedeemPointsCommandHandler(IPointsRepository repository, IApplicationEventPublisher applicationEventPublisher) : base(repository, applicationEventPublisher)
        {
        }

        public override Task<bool> HandleCommandAsync(Points points, RedeemPoints command)
        {
            points.Redeem(command.Points);

            return Task.FromResult(true);
        }

        public override IEnumerable<IApplicationEvent> RaiseApplicationEvents(Points points, RedeemPoints command)
        {
            return new IApplicationEvent[] {
                new PointsRedeemedEvent(command, command.PointsId)
            };
        }
    }
}
