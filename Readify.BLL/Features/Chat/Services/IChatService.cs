﻿using Readify.BLL.Features.Chat.DTOs;
using System.Threading.Tasks;
using Readify.DAL.Entities;
using Readify.BLL.Specifications.ChatSpec;

namespace Readify.BLL.Features.Chat.Services
{
    public interface IChatService
    {
        Task<List<string>> AddMessageAsync(IncomingMessageDto message);
        Task<ConversationsListDto> GetAllConversationsAsync(ChatSpecification specs);
        Task<ConversationDto> GetConversationWithUserAsync(string userId);
    }
}
