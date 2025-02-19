using chiai.Server.Data;
using chiai.Server.Data.Dto;
using chiai.Server.Sevices.Abstracts;

namespace chiai.Server.Sevices.Implementations
{
    public class ChatService : IChatService
    {
        private ApplicationDbContext _dbContext;
        private ILogger<ChatService> _logger;

        public ChatService(ApplicationDbContext context, ILogger<ChatService> logger)
        {
            _dbContext = context;
            _logger = logger;
        }
        public async Task<ChatDto> StartNewChat(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError($"User with id {userId} not found");
                throw new ArgumentException("User not found");
            }

            var chat = new Chat
            {
                UserId = userId,
            };
            chat.Title = $"Chat no. {_dbContext.Chats.Max(x => x.Id)+1}";

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Chats.Add(chat);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return ChatDto.FromChat(chat);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to start new chat");
                    transaction.Rollback();
                    throw new ArgumentException("Failed to start new chat");
                }

            }
        }
    }
}
