using Readify.API.HandleResponses;
using Readify.BLL.Features.Chat.DTOs;
using Readify.BLL.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.ResponseExample.Chat
{
    public class ConversationsListDtoExample : IExamplesProvider<ApiResponse<ConversationsListDto>>
    {
        public ApiResponse<ConversationsListDto> GetExamples()
        {
            return new ApiResponse<ConversationsListDto>(200, "Conversations retrieved successfully", new ConversationsListDto
            {
                Conversations = new List<ChatDto>
            {
                new ChatDto
                {
                    ConversationId = 1,
                    UserId = "user123",
                    UserName = "John Doe"
                },
                new ChatDto
                {
                    ConversationId = 2,
                    UserId = "user456",
                    UserName = "Jane Smith"
                }
            },
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageIndex = 1,
                        PageSize = 10,
                        TotalRecords = 2,
                        TotalPages = 1
                    }
                }
            });
        }
    }
    public class ConversationDtoExample : IExamplesProvider<ApiResponse<ConversationDto>>
    {
        public ApiResponse<ConversationDto> GetExamples()
        {
            return new ApiResponse<ConversationDto>(200, "Conversation retrieved successfully", new ConversationDto
            {
                Id = 5,
                UserId = "user-123",
                MessageCount = 2,
                Messages = new List<MessageDto>
            {
                new MessageDto
                {
                    SenderType = UserType.User,
                    UserId = "user-123",
                    LibrarianId = null,
                    Content = "Hi, I need help with my book.",
                    SentTime = DateTime.UtcNow.AddMinutes(-30)
                },
                new MessageDto
                {
                    SenderType = UserType.Librarian,
                    UserId = null,
                    LibrarianId = "librarian-456",
                    Content = "Sure! What seems to be the issue?",
                    SentTime = DateTime.UtcNow.AddMinutes(-28)
                }
            }
            });
        }
    }

}
