using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.CQRS.Queries.SearchWallet;

namespace Htec.Poc.API.Controllers;

/// <summary>
/// Wallet related operations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[ApiExplorerSettings(GroupName = "Wallet", IgnoreApi = true)]
[ApiController]
public class SearchWalletController : ApiControllerBase
{
    readonly IQueryHandler<SearchWallet, SearchWalletResult> queryHandler;

    public SearchWalletController(IQueryHandler<SearchWallet, SearchWalletResult> queryHandler)
    {
        this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }


    /// <summary>
    /// Get or search a list of wallets
    /// </summary>
    /// <remarks>By passing in the appropriate options, you can search for available wallets in the system </remarks>
    /// <param name="searchTerm">pass an optional search string for looking up wallets</param>
    /// <param name="RestaurantId">id of restaurant to look up for wallet's</param>
    /// <param name="pageNumber">page number</param>
    /// <param name="pageSize">maximum number of records to return per page</param>
    /// <response code="200">search results matching criteria</response>
    /// <response code="400">bad request</response>
    [HttpGet("/v1/wallet/")]
    [Authorize]
    [ProducesResponseType(typeof(SearchWalletResponse), 200)]
    public async Task<IActionResult> SearchWallet(
        [FromQuery]string searchTerm,
        [FromQuery]Guid? RestaurantId,
        [FromQuery][Range(0, 50)]int? pageSize = 20,
        [FromQuery]int? pageNumber = 1)
    {
        // NOTE: Please ensure the API returns the response codes annotated above

        var criteria = new SearchWallet(
            correlationId: GetCorrelationId(),
            searchText: searchTerm,
            restaurantId: RestaurantId,
            pageSize: pageSize,
            pageNumber: pageNumber
        );

        var result = await queryHandler.ExecuteAsync(criteria);

        var response = SearchWalletResponse.FromWalletResultItem(result);

        return new ObjectResult(response);
    }
}
