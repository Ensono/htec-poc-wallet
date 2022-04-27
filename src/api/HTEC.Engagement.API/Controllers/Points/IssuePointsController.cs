using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.Engagement.API.Models.Requests;
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
    public class IssuePointsController : ApiControllerBase
    {
        readonly ICommandHandler<IssuePoints, bool> commandHandler;

        public IssuePointsController(ICommandHandler<IssuePoints, bool> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpPut("/v1/points/issue/{id}")]
        [Authorize]
        public async Task<IActionResult> IssuePoints([FromRoute][Required] Guid id, [FromBody] IssuePointsRequest body)
        {
            await commandHandler.HandleAsync(
                new IssuePoints()
                {
                    PointsId = id,
                    Points = body.Points
                });

            return StatusCode(204);
        }
    }
}
