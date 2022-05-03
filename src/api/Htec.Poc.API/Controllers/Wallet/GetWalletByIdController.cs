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
[ApiExplorerSettings(GroupName = "Wallet", IgnoreApi = true)]
[ApiController]
public class GetWalletByIdController : ApiControllerBase
{
    readonly IQueryHandler<Query.GetWalletById, Query.Wallet> queryHandler;

    public GetWalletByIdController(IQueryHandler<Query.GetWalletById, Query.Wallet> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }

    /// <summary>
    /// Get a wallet
    /// </summary>
    /// <remarks>By passing the wallet id, you can get access to available categories and items in the wallet </remarks>
    /// <param name="id">wallet id</param>
    /// <response code="200">Wallet</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Resource not found</response>
    [HttpGet("/v1/wallet/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Wallet), 200)]
    public async Task<IActionResult> GetWallet([FromRoute][Required]Guid id)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var result = await queryHandler.ExecuteAsync(new Query.GetWalletById() { Id = id });

        if (result == null)
            return NotFound();

        var wallet = Wallet.FromQuery(result);

        return new ObjectResult(wallet);
    }
}
