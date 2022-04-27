using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.CQRS.Queries.GetPointsById;

namespace HTEC.Engagement.Application.QueryHandlers
{
    public class GetPointsByIdQueryHandler : IQueryHandler<GetPointsById, Points>
    {
        private readonly IPointsRepository repository;

        public GetPointsByIdQueryHandler(IPointsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Points> ExecuteAsync(GetPointsById criteria)
        {
            var points = await repository.GetByIdAsync(criteria.Id);

            if (points == null)
                return null;

            //You might prefer using AutoMapper in here
            var result = new Points()
            {
                Id = points.Id,
                TenantId = points.TenantId,
                Name = points.Name,
                Description = points.Description,
                Enabled = points.Enabled,
                Balance = points.Balance
            };

            return result;
        }
    }
}
