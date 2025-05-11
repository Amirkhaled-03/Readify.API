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
                Conversations = new List<ConversationDto>
            {
                new ConversationDto
                {
                    Id = 1,
                    UserId = "101",
                    MessageCount = 2,
                    Messages = new List<MessageDto>
                    {
                        new MessageDto
                        {
                            SenderType = UserType.User,
                            UserId = "101",
                            LibrarianId = "102",
                            Content = "Hello!",
                            SentTime = DateTime.UtcNow.AddMinutes(-10)
                        },
                        new MessageDto
                        {
                            SenderType = UserType.Librarian,
                            UserId = "101",
                            LibrarianId = "102",
                            Content = "Hi there!",
                            SentTime = DateTime.UtcNow.AddMinutes(-8)
                        }
                    }
                }
            },
                Metadata = new Metadata
                {
                    Pagination = new Pagination
                    {
                        PageIndex = 1,
                        PageSize = 10,
                        TotalRecords = 1,
                        TotalPages = 1
                    }
                }
            });
        }
    }
}
