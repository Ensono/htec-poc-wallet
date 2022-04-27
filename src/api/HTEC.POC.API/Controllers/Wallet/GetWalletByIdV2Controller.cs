using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.API.Models.Responses;

namespace HTEC.POC.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Wallet")]
[ApiController]
public class GetWalletByIdV2Controller : ApiControllerBase
{
    /// <summary>
    /// Get a wallet
    /// </summary>
    /// <remarks>By passing the wallet id, you can get access to available categories and items in the wallet </remarks>
    /// <param name="id">wallet id</param>
    /// <response code="200">Wallet</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v2/wallet/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Wallet), 200)]
    public virtual IActionResult GetWalletV2([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        return BadRequest();
    }
}
