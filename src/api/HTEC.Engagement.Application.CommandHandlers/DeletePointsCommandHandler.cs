using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using HTEC.Engagement.Application.CQRS.Events;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.Common.Exceptions;
using HTEC.Engagement.CQRS.Commands;

namespace HTEC.Engagement.Application.CommandHandlers
{
    public class DeletePointsCommandHandler : ICommandHandler<DeletePoints, bool>
    {
        readonly IPointsRepository repository;
        readonly IApplicationEventPublisher applicationEventPublisher;

        public DeletePointsCommandHandler(IPointsRepository repository, IApplicationEventPublisher applicationEventPublisher)
        {
            this.repository = repository;
            this.applicationEventPublisher = applicationEventPublisher;
        }

        public async Task<bool> HandleAsync(DeletePoints command)
        {
            var points = await repository.GetByIdAsync(command.PointsId);

            if (points == null)
                PointsDoesNotExistException.Raise(command, command.PointsId);

            // TODO: Check if the user owns the resource before any operation
            // if(command.User.TenantId != points.TenantId)
            // {
            //     throw NotAuthorizedException()
            // }

            var successful = await repository.DeleteAsync(command.PointsId);

            if (!successful)
                OperationFailedException.Raise(command, command.PointsId, "Unable to delete points");

            await applicationEventPublisher.PublishAsync(
                new PointsDeletedEvent(command, command.PointsId)
            );

            return successful;
        }
    }
}
