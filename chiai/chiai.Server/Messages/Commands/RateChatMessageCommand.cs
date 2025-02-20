using chiai.Server.Data;
using chiai.Server.Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chiai.Server.Messages.Commands
{
    public record RateChatMessageCommand(int chatId, int messageId, int rating) : IRequest<Unit>;

    public class RateChatMessageCommandHandler : IRequestHandler<RateChatMessageCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RateChatMessageCommandHandler> _logger;
        public RateChatMessageCommandHandler(ApplicationDbContext dbContext, ILogger<RateChatMessageCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Unit> Handle(RateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == request.chatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {request.chatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var message = chat.Messages.FirstOrDefault(m => m.Id == request.messageId);
            if (message == null)
            {
                _logger.LogError($"Message with id {request.messageId} not found");
                throw new ArgumentException("Message not found");
            }

            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    message.Rating = (RatingType)request.rating;
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
            return Unit.Value;
        }
    }
}
