using Microsoft.AspNetCore.Mvc;
using APIGatewayRouting.Routing.Interfaces;
using APIGatewayControllers.DataMappers;
using APIGatewayRouting.Data;
using APIGatewayControllers.Models;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentCommentController : ControllerBase
    {
        private readonly ILogger<ContentCommentController> _logger;
        private readonly IContentCommentRouter _contentCommentRouter;

        public ContentCommentController(
            ILogger<ContentCommentController> logger,
            IContentCommentRouter contentCommentRouter)
        {
            _logger = logger;
            _contentCommentRouter = contentCommentRouter;
        }

        [HttpPost("Add", Name = "AddComment")]
        public async Task<ActionResult<string>> AddCommentAsync(ContentCommentModel contentCommentModel)
        {
            try
            {
                bool result = await _contentCommentRouter.AddCommentAsync(contentCommentModel.ToContentComment());
                if (result)
                {
                    return StatusCode(500, $"Operation add comment can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"Comment has been added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                return StatusCode(500, $"An error occurred while adding a user. Error message: {ex.Message}");
            }
        }

        [HttpDelete("Delete", Name = "RemoveComment")]
        public async Task<ActionResult<string>> DeleteCommentAsync(Guid commentId)
        {

            try
            {
                bool result = await _contentCommentRouter.RemoveCommentAsync(commentId);
                if (!result)
                {
                    return NotFound($"Comment with id: {commentId} doesn't exist!"); //TODO: or another ret code?? Use exceptions??
                }

                return Ok($"Comment with id: {commentId} has been removed successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while removing a comment.");
                return StatusCode(500, $"An error occurred while removing a comment. Error message: {ex.Message}");
            }
        }

        [HttpPut("Edit", Name = "EditComment")] 
        public async Task<ActionResult<string>> EditCommentAsync(Guid commentId, ContentCommentModel contentCommentModel)
        {
            try
            {
                bool result = await _contentCommentRouter.EditCommentAsync(commentId, contentCommentModel.ToContentComment());
                if (!result)
                {
                    return StatusCode(500, $"Operation edit comment can not be completed at the moment. Pleas try again later or contact the administrator for more information.");
                }

                return Ok($"Comment with id {commentId} has been changed successfully.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while editing a comment.");
                return StatusCode(500, $"An error occurred while editing a comment. Error message: {ex.Message}");
            }
        }

    }
}
