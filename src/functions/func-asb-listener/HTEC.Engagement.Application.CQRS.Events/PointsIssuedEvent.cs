using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class PointsIssuedEvent : IApplicationEvent
    {
        [JsonConstructor]
        public PointsIssuedEvent(int operationCode, Guid correlationId, Guid pointsId, int points)
        {
            OperationCode = operationCode;
            CorrelationId = correlationId;
            PointsId = pointsId;
            Points = points;
        }

        public PointsIssuedEvent(IOperationContext context, Guid pointsId, int points)
        {
            OperationCode = context.OperationCode;
            CorrelationId = context.CorrelationId;
            PointsId = pointsId;
            Points = points;
        }

        public int EventCode => (int)Enums.EventCode.PointsIssued;

        public int OperationCode { get; }

        public Guid CorrelationId { get; }

        public Guid PointsId { get; set; }

        public int Points { get; set; }
    }
}
