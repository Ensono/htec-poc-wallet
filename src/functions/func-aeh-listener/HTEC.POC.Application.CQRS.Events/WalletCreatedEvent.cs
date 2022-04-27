using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace HTEC.POC.Application.CQRS.Events
{
	public class WalletCreatedEvent : IApplicationEvent
	{
		[JsonConstructor]
		public WalletCreatedEvent(int operationCode, Guid correlationId, Guid walletId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
			WalletId = walletId;
		}

		public WalletCreatedEvent(IOperationContext context, Guid walletId)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
			WalletId = walletId;
		}

		public int EventCode => (int)Enums.EventCode.WalletCreated;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }

		public Guid WalletId { get; set; }
	}
}
