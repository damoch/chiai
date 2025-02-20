using chiai.Server.Data;
using chiai.Server.Data.Dto;
using chiai.Server.Data.Enums;
using chiai.Server.Sevices.Abstracts;
using Microsoft.EntityFrameworkCore;

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

        public async Task RateMessageAsync(int chatId, int messageId, int rating)
        {
            var chat = _dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {chatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var message = _dbContext.ChatMessages.FirstOrDefault(m => m.Id == messageId);
            if (message == null)
            {
                _logger.LogError($"Message with id {messageId} not found");
                throw new ArgumentException("Message not found");
            }
            if (!message.IsFromAi)
            {
                _logger.LogError($"Cannot rate non-AI message");
                throw new ArgumentException("Cannot rate non-AI message");
            }
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    message.Rating = (RatingType)rating;
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to rate message");
                    transaction.Rollback();
                    throw new ArgumentException("Failed to rate message");
                }
            }
        }

        public async Task SaveMessageAsync(int chatId, ChatMessageDto message)
        {
            var chat = _dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {chatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var chatMessage = new ChatMessage
            {
                ChatId = chatId,
                Content = message.Content,
                Author = message.Author,
                Timestamp = DateTime.Now,
                IsFromAi = message.IsFromAi,
                Rating = RatingType.None
            };
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.ChatMessages.Add(chatMessage);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save message");
                    transaction.Rollback();
                    throw new ArgumentException("Failed to save message");
                }
            }
        }

        public async Task<ChatDto> StartNewChat(int userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError($"User with id {userId} not found");
                throw new ArgumentException("User not found");
            }

            // Determine the next chat number efficiently
            int nextChatNumber = (await _dbContext.Chats.MaxAsync(x => (int?)x.Id) ?? 0) + 1;

            var chat = new Chat
            {
                UserId = userId,
                Title = $"Chat no. {nextChatNumber}"
            };

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.Chats.Add(chat);
                await _dbContext.SaveChangesAsync(); // Chat ID is now assigned
                await transaction.CommitAsync();

                return ChatDto.FromChat(chat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start new chat");
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Failed to start new chat", ex);
            }
        }

    }
}
