using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class PointsUpdatedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public PointsUpdatedEvent(int operationCode, Guid correlationId, Guid pointsId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			PointsId = pointsId;
		}

		public PointsUpdatedEvent(IOperationContext context, Guid pointsId)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			PointsId = pointsId;
		}

		public int EventCode => (int)Enums.EventCode.PointsUpdated;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid PointsId { get; set; }
	}
}
