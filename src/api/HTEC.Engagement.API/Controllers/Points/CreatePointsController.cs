using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.Engagement.API.Models.Requests;
using HTEC.Engagement.API.Models.Responses;
using HTEC.Engagement.CQRS.Commands;

namespace HTEC.Engagement.API.Controllers
{
    /// <summary>
    /// Points related operations
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Points")]
    [ApiController]
    public class CreatePointsController : ApiControllerBase
    {
        readonly ICommandHandler<CreatePoints, Guid> commandHandler;

        public CreatePointsController(ICommandHandler<CreatePoints, Guid> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        /// <summary>
        /// Create a points
        /// </summary>
        /// <remarks>Adds a points</remarks>
        /// <param name="body">Points being created</param>
        /// <response code="201">Resource created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict, an item already exists</response>
        [HttpPost("/v1/points/")]
        [Authorize]
        [ProducesResponseType(typeof(ResourceCreatedResponse), 201)]
        public async Task<IActionResult> CreatePoints([Required][FromBody]CreatePointsRequest body)
        {
            // NOTE: Please ensure the API returns the response codes annotated above

            var id = await commandHandler.HandleAsync(
                new CreatePoints(
                        correlationId: GetCorrelationId(),
                        tenantId: body.TenantId, //Should check if user logged-in owns it
                        name: body.Name,
                        description: body.Description,
                        enabled: body.Enabled,
                        balance: body.Balance
                    )
                );

            return new CreatedAtActionResult(
                    "GetPoints", "GetPointsById", new
                    {
                        id = id
                    }, new ResourceCreatedResponse(id)
            );
        }
    }
}
