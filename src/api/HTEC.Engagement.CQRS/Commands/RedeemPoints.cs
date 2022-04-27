using System;

namespace HTEC.Engagement.CQRS.Commands
{
    public class RedeemPoints : IPointsCommand
    {
        public int OperationCode => (int)Common.Operations.OperationCode.RedeemPoints;

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public int Points { get; set; }
    }

}
