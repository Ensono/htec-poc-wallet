﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.CQRS.Commands;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Wallet", IgnoreApi = true)]
[ApiController]
public class UpdateWalletController : ApiControllerBase
{
    readonly ICommandHandler<UpdateWallet, bool> commandHandler;

    public UpdateWalletController(ICommandHandler<UpdateWallet, bool> commandHandler)
    {
        this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }


    /// <summary>
    /// Update a wallet
    /// </summary>
    /// <remarks>Update a wallet with new information</remarks>
    /// <param name="id">wallet id</param>
    /// <param name="body">Wallet being updated</param>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpPut("/v1/wallet/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateWallet([FromRoute][Required]Guid id, [FromBody]UpdateWalletRequest body)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        await commandHandler.HandleAsync(
            new UpdateWallet()
            {
                WalletId = id,
                Name = body.Name,
                Enabled = body.Enabled,
                Points = body.Points
            });

        return StatusCode(204);
    }
}
