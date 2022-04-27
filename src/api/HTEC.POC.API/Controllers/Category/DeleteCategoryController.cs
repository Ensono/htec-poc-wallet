using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.Controllers
    // ReSharper disable once ArrangeNamespaceBody
{
    /// <summary>
    /// Category related operations
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Category")]
    public class DeleteCategoryController : ApiControllerBase
    {
        readonly ICommandHandler<DeleteCategory, bool> commandHandler;

        public DeleteCategoryController(ICommandHandler<DeleteCategory, bool> commandHandler)
        {
            this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
        }


        /// <summary>
        /// Removes a category and its items from wallet
        /// </summary>
        /// <remarks>Removes a category and its items from wallet</remarks>
        /// <param name="id">wallet id</param>
        /// <param name="categoryId">Id for Category being removed</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Resource not found</response>
        [HttpDelete("/v1/wallet/{id}/category/{categoryId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromRoute][Required]Guid id, [FromRoute][Required]Guid categoryId)
        {
            // NOTE: Please ensure the API returns the response codes annotated above

            await commandHandler.HandleAsync(
                new DeleteCategory(
                    correlationId: GetCorrelationId(),
                    walletId: id,
                    categoryId: categoryId
                )
            );

            return StatusCode(204);
        }
    }
}
