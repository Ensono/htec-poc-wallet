using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Responses;
using Query = Htec.Poc.CQRS.Queries.GetWalletById;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Wallet")]
[ApiController]
public class GetWalletByMemberIdController : ApiControllerBase
{
    public GetWalletByMemberIdController()
    {
        
    }

    /// <summary>
    /// Get a wallet by member id
    /// </summary>
    /// <remarks>By passing the member id, you can get access to the wallet. </remarks>
    /// <param name="memberId">member id</param>
    /// <response code="200">Wallet</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v1/members/{memberId}/wallet")]
    [Authorize]
    [ProducesResponseType(typeof(Wallet), 200)]
    public async Task<IActionResult> GetWalletByMemberId([FromRoute][Required] Guid memberId)
    {
        return new ObjectResult(new Wallet());
    }
}
