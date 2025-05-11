using Microsoft.AspNetCore.Mvc;
using Readify.API.HandleResponses;
using Readify.API.ResponseExample.Chat;
using Readify.BLL.Features.Chat.DTOs;
using Readify.BLL.Features.Chat.Services;
using Readify.BLL.Specifications.ChatSpec;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService; 
        }

        #region Get All Conversations

        [HttpGet("GetAllConversations")]
        [SwaggerOperation(
            Summary = "Get all conversations with pagination and filters",
            Description = "Retrieves all conversations matching the provided specification, including messages and metadata for pagination.")]
        [SwaggerResponse(200, "Conversations retrieved successfully", typeof(ApiResponse<ConversationsListDto>))]
        [SwaggerResponse(204, "No conversations found")]
        [SwaggerResponseExample(200, typeof(ConversationsListDtoExample))]
        public async Task<IActionResult> GetAllConversations([FromQuery] ChatSpecification specs)
        {
            var conversations = await _chatService.GetAllConversationsAsync(specs);

            if (conversations.Conversations == null || !conversations.Conversations.Any())
                return NoContent();

            return Ok(new ApiResponse<ConversationsListDto>(200, "Conversations retrieved successfully", conversations));
        }

        #endregion

    }
}
