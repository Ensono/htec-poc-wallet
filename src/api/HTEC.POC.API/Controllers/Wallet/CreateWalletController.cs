using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.API.Models.Responses;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Wallet")]
[ApiController]
public class CreateWalletController : ApiControllerBase
{
    readonly ICommandHandler<CreateWallet, Guid> commandHandler;

    public CreateWalletController(ICommandHandler<CreateWallet, Guid> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Create a wallet
    /// </summary>
    /// <remarks>Adds a wallet</remarks>
    /// <param name="body">Wallet being created</param>
    /// <response code="201">Resource created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="409">Conflict, an item already exists</response>
    [HttpPost("/v1/wallet/")]
    [Authorize]
    [ProducesResponseType(typeof(ResourceCreatedResponse), 201)]
    public async Task<IActionResult> CreateWallet([Required][FromBody]CreateWalletRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var id = await commandHandler.HandleAsync(
            new CreateWallet(
                correlationId: GetCorrelationId(),
                tenantId: body.TenantId, //Should check if user logged-in owns it
                name: body.Name,
                description: body.Description,
                enabled: body.Enabled
            )
        );

        return new CreatedAtActionResult(
            "GetWallet", "GetWalletById", new
            {
                id = id
            }, new ResourceCreatedResponse(id)
        );
    }
}
