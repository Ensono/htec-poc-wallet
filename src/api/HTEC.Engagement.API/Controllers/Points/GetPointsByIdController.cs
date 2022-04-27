using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.Engagement.API.Models.Responses;
using Query = HTEC.Engagement.CQRS.Queries.GetPointsById;

namespace HTEC.Engagement.API.Controllers
{
    /// <summary>
    /// Points related operations
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Points")]
    [ApiController]
    public class GetPointsByIdController : ApiControllerBase
    {
        readonly IQueryHandler<Query.GetPointsById, Query.Points> queryHandler;

        public GetPointsByIdController(IQueryHandler<Query.GetPointsById, Query.Points> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        /// <summary>
        /// Get a points
        /// </summary>
        /// <remarks>By passing the points id, you can get access to available categories and items in the points </remarks>
        /// <param name="id">points id</param>
        /// <response code="200">Points</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Resource not found</response>
        [HttpGet("/v1/points/view/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Points), 200)]
        public async Task<IActionResult> GetPoints([FromRoute][Required]Guid id)
        {
            // NOTE: Please ensure the API returns the response codes annotated above

            var result = await queryHandler.ExecuteAsync(new Query.GetPointsById() { Id = id });

            if (result == null)
                return NotFound();

            var points = new Points
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Balance = result.Balance,
                Enabled = result.Enabled
            };

            return new ObjectResult(points);
        }
    }
}
