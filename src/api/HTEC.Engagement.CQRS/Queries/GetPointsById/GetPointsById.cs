using System;
using Amido.Stacks.Application.CQRS.Queries;

namespace HTEC.Engagement.CQRS.Queries.GetPointsById
{
    public class GetPointsById : IQueryCriteria
    {
        public int OperationCode => (int)Common.Operations.OperationCode.GetPointsById;

        public Guid CorrelationId { get; }

        public Guid Id { get; set; }
    }
}
