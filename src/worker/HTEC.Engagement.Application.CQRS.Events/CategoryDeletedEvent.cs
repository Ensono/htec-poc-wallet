using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.Engagement.Application.CQRS.Events
{
    public class CategoryDeletedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public CategoryDeletedEvent(int operationCode, Guid correlationId, Guid pointsId, Guid categoryId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			PointsId = pointsId;
			CategoryId = categoryId;
		}

		public CategoryDeletedEvent(IOperationContext context, Guid pointsId, Guid categoryId)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			PointsId = pointsId;
			CategoryId = categoryId;
		}

		public int EventCode => (int)Enums.EventCode.CategoryDeleted;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid PointsId { get; set; }

		public Guid CategoryId { get; set; }
	}
}
