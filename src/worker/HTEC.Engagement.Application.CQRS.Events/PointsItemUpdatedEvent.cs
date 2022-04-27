using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class PointsItemUpdatedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public PointsItemUpdatedEvent(int operationCode, Guid correlationId, Guid pointsId, Guid categoryId, Guid pointsItemId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			PointsId = pointsId;
			CategoryId = categoryId;
			PointsItemId = pointsItemId;
		}

		public PointsItemUpdatedEvent(IOperationContext context, Guid pointsId, Guid categoryId, Guid pointsItemId)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			PointsId = pointsId;
			CategoryId = categoryId;
			PointsItemId = pointsItemId;
		}

		public int EventCode => (int)Enums.EventCode.PointsItemUpdated;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid PointsId { get; set; }

		public Guid CategoryId { get; set; }

		public Guid PointsItemId { get; set; }
	}
}
