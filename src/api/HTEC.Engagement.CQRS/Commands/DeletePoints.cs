using System;

namespace HTEC.Engagement.CQRS.Commands
{
    public class DeletePoints : IPointsCommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.DeletePoints;

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public DeletePoints(Guid pointsId)
        {
            this.PointsId = pointsId;
        }
    }
}
