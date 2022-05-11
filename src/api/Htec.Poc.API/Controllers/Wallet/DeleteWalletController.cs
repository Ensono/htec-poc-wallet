using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Wallet", IgnoreApi = true)]
[ApiController]
public class DeleteWalletController : ApiControllerBase
{
    readonly ICommandHandler<DeleteWallet, bool> commandHandler;

    public DeleteWalletController(ICommandHandler<DeleteWallet, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Removes a wallet with all its categories and items
    /// </summary>
    /// <remarks>Remove a wallet from a restaurant</remarks>
    /// <param name="id">wallet id</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpDelete("/v1/wallet/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteWallet([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(new DeleteWallet(id));
        return StatusCode(204);
    }
}
