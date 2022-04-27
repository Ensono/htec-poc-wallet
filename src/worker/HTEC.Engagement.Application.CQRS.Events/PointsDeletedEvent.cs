using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class PointsDeletedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public PointsDeletedEvent(int operationCode, Guid correlationId, Guid pointsId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			PointsId = pointsId;
		}

		public PointsDeletedEvent(IOperationContext context, Guid pointsId)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			PointsId = pointsId;
		}

		public int EventCode => (int)Enums.EventCode.PointsDeleted;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid PointsId { get; set; }
	}
}
