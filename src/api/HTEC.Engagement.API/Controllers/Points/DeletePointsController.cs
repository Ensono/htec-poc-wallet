using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.Engagement.CQRS.Commands;

namespace HTEC.Engagement.API.Controllers
{
    /// <summary>
    /// Points related operations
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Points")]
    [ApiController]
    public class DeletePointsController : ApiControllerBase
    {
        readonly ICommandHandler<DeletePoints, bool> commandHandler;

        public DeletePointsController(ICommandHandler<DeletePoints, bool> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        /// <summary>
        /// Removes a points with all its categories and items
        /// </summary>
        /// <remarks>Remove a points from a restaurant</remarks>
        /// <param name="id">points id</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Resource not found</response>
        [HttpDelete("/v1/points/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePoints([FromRoute][Required]Guid id)
        {
            // NOTE: Please ensure the API returns the response codes annotated above

            await commandHandler.HandleAsync(new DeletePoints(id));
            return StatusCode(204);
        }
    }
}
