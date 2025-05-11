using Readify.BLL.Features.Chat.DTOs;
using Readify.BLL.Helpers;
using Readify.BLL.Specifications.ChatSpec;
using Readify.DAL.Entities;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Chat.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public ChatService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<string>> AddMessageAsync(IncomingMessageDto incomingMessage)
        {
            List<string> errors = new();
            var existingConversation = await _unitOfWork.ConversationRepository.GetByUserAsync(incomingMessage.UserId);

            int conversationId;

            if (existingConversation != null)
            {
                conversationId = existingConversation.Id;
            }
            else
            {
                var newConversation = new Conversation
                {
                    UserId = incomingMessage.UserId,
                };

                _unitOfWork.ConversationRepository.AddEntity(newConversation);
                int convoResult = await _unitOfWork.SaveAsync();

                if (convoResult <= 0)
                {
                    errors.Add("An error occurred while creating a new conversation.");
                    return errors;
                }

                conversationId = newConversation.Id;
            }

            var message = new Message
            {
                ConversationId = conversationId,
                SenderType = incomingMessage.SenderType,
                UserId = incomingMessage.UserId,
                LibrarianId = incomingMessage.LibrarianId,
                Content = incomingMessage.Content,
                SentTime = incomingMessage.SentTime,
            };

            _unitOfWork.MessageRepository.AddEntity(message);

            int affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                errors.Add("An error occurred while saving the message.");
            }

            return errors;
        }

        public async Task<ConversationsListDto> GetAllConversationsAsync(ChatSpecification specs)
        {
            var totalCountSpec = new ChatSpecificationImpl(specs);
            totalCountSpec.IgnorePagination();

            int matchedFilterationCount = await _unitOfWork.ConversationRepository.CountWithSpecAsync(totalCountSpec);
            int totalCount = await _unitOfWork.ConversationRepository.CountAsync();

            var specifications = new ChatSpecificationImpl(specs);

            var conversations = await _unitOfWork.ConversationRepository.GetWithSpecificationsAsync(specifications)
               ?? Enumerable.Empty<Conversation>();

            var conversationDto = conversations.Select(c => new ConversationDto
            {
                Id = c.Id,
                UserId = c.UserId,
                MessageCount = c.Messages.Count,
                Messages = c.Messages?.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderType = m.SenderType,
                    UserId = m.UserId,
                    LibrarianId = m.LibrarianId,
                    Content = m.Content,
                    SentTime = m.SentTime
                }).ToList() ?? new List<MessageDto>()
            });

            var pagination = new Pagination
            {
                PageIndex = specs.PageIndex,
                PageSize = specs.PageSize,
                TotalRecords = matchedFilterationCount,
                TotalPages = (int)Math.Ceiling((double)matchedFilterationCount / specs.PageSize)
            };

            return new ConversationsListDto
            {
                Conversations = conversationDto.ToList(),
                Metadata = new Metadata()
                {
                    Pagination = pagination,
                }
            };
        }

        public Task<List<MessageDto>> GetMessagesAsync(int conversationId)
        {
            throw new NotImplementedException();
        }

        public Task<ConversationDto> GetOrCreateConversationAsync(string user1Id, string user2Id)
        {
            throw new NotImplementedException();
        }
    }
}
