using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Commands;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class CreatePointsCommandHandler : ICommandHandler<CreatePoints, Guid>
    {
        private readonly IPointsRepository repository;
        private readonly IApplicationEventPublisher applicationEventPublisher;

        public CreatePointsCommandHandler(IPointsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        {
            this.repository = repository;
            this.applicationEventPublisher = applicationEventPublisher;
        }

        public async Task<Guid> HandleAsync(CreatePoints command)
        {
            var id = Guid.NewGuid();

            // TODO: Check if the user owns the resource before any operation
            // if(command.User.TenantId != points.TenantId)
            // {
            //     throw NotAuthorizedException()
            // }


            var newPoints = new Points(
                id: id,
                name: command.Name,
                tenantId: command.TenantId,
                description: command.Description,
                enabled: command.Enabled,
                balance: command.Balance
            );

            await repository.SaveAsync(newPoints);

            await applicationEventPublisher.PublishAsync(new PointsCreatedEvent(command, id));

            return id;
        }
    }
}
