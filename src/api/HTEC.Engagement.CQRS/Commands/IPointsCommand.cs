using System;
using Amido.Stacks.Application.CQRS.Commands;

namespace HTEC.Engagement.CQRS.Commands
{
    /// <summary>
    /// Define required parameters for commands executed against a points
    /// </summary>
    public interface IPointsCommand : ICommand
    {
        Guid PointsId { get; }
    }
}
