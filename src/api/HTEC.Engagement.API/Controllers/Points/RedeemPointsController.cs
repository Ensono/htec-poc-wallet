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
    public class RedeemPointsController : ApiControllerBase
    {
        readonly ICommandHandler<RedeemPoints, bool> commandHandler;

        public RedeemPointsController(ICommandHandler<RedeemPoints, bool> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpPut("/v1/points/redeem/{id}")]
        [Authorize]
        public async Task<IActionResult> IssuePoints([FromRoute][Required] Guid id, [FromBody] RedeemPointsRequest body)
        {
            await commandHandler.HandleAsync(
                new RedeemPoints()
                {
                    PointsId = id,
                    Points = body.Points
                });

            return StatusCode(204);
        }
    }
}
