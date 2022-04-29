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

using Microsoft.AspNetCore.Http;

/// <summary>
/// Category related operations
/// </summary>
[Consumes("application/json")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Category")]
public class AddWalletCategoryController : ApiControllerBase
{
    readonly ICommandHandler<CreateCategory, Guid> commandHandler;

    public AddWalletCategoryController(ICommandHandler<CreateCategory, Guid> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Create a category in the wallet
    /// </summary>
    /// <remarks>Adds a category to wallet</remarks>
    /// <param name="id">wallet id</param>
    /// <param name="body">Category being added</param>
    /// <response code="201">Resource created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    /// <response code="409">Conflict, an item already exists</response>
    [HttpPost("/v1/wallet/{id}/category/")]
    [Authorize]
    [ProducesResponseType(typeof(ResourceCreatedResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddWalletCategory([FromRoute][Required]Guid id, [FromBody]CreateCategoryRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var categoryId = await commandHandler.HandleAsync(
            new CreateCategory(
                correlationId: GetCorrelationId(),
                walletId: id,
                name: body.Name,
                description: body.Description
            )
        );

        return StatusCode(StatusCodes.Status201Created, new ResourceCreatedResponse(categoryId));
    }
}
