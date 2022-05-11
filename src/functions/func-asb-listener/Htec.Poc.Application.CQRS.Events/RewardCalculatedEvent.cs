using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Htec.Poc.Application.CQRS.Events
{
    public class RewardCalculatedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public RewardCalculatedEvent(int operationCode, Guid correlationId, Guid memberId, int points)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			MemberId = memberId;
			Points = points;
		}

		public RewardCalculatedEvent(IOperationContext context, Guid memberId, int points)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			MemberId = memberId;
			Points = points;
		}

		public int EventCode => (int)Enums.EventCode.RewardCalculated;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid MemberId { get; }

		public int Points { get; }
	}
}
