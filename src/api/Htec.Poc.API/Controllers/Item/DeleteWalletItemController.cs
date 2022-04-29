using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Item related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Item")]
public class DeleteWalletItemController : ApiControllerBase
{
    readonly ICommandHandler<DeleteWalletItem, bool> commandHandler;

    public DeleteWalletItemController(ICommandHandler<DeleteWalletItem, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Removes an item from wallet
    /// </summary>
    /// <remarks>Removes an item from wallet</remarks>
    /// <param name="id">wallet id</param>
    /// <param name="categoryId">Category ID</param>
    /// <param name="itemId">Id for Item being removed</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpDelete("/v1/wallet/{id}/category/{categoryId}/items/{itemId}")]
    [Authorize]
    public async Task<IActionResult> DeleteWalletItem([FromRoute][Required]Guid id, [FromRoute][Required]Guid categoryId, [FromRoute][Required]Guid itemId)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new DeleteWalletItem(
                correlationId: GetCorrelationId(),
                walletId: id,
                categoryId: categoryId,
                walletItemId: itemId
            )
        );

        return StatusCode(204);
    }
}
