using chiai.Server.Data;
using chiai.Server.Data.Dto;
using chiai.Server.Sevices.Abstracts;

namespace chiai.Server.Sevices.Implementations
{
    public class HistoryService : IHistoryService
    {
        private ApplicationDbContext _dbContext;
        private ILogger<IHistoryService> _logger;

        public HistoryService(ApplicationDbContext dbContext, ILogger<IHistoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public List<ChatDto> GetHistory(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogError($"User with id {userId} not found");
                throw new ArgumentException("User not found");
            }
            var chats = _dbContext.Chats.Where(c => c.UserId == userId).ToList();
            return chats.Select(ChatDto.FromChat).ToList();
        }

        public List<ChatMessageDto> GetChatMessages(int chatId)
        {
            var chat = _dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {chatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var messages = _dbContext.ChatMessages.Where(m => m.ChatId == chatId).ToList();
            return messages.Select(ChatMessageDto.FromChatMessage).ToList();
        }
    }
}
