using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Wallet", IgnoreApi = true)]
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
    public async Task<IActionResult> CreateWallet([Required][FromBody] CreateWalletRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var id = await commandHandler.HandleAsync(
            new CreateWallet(
                correlationId: GetCorrelationId(),
                name: body.Name,
                enabled: body.Enabled,
                points: body.Points
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
