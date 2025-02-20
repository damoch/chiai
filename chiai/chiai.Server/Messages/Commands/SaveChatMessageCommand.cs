using chiai.Server.Data;
using MediatR;

namespace chiai.Server.Messages.Commands
{
    public record SaveChatMessageCommand(int ChatId, ChatMessageDto Message) : IRequest<Unit>;
  
    public class SaveChatMessageCommandHandler : IRequestHandler<SaveChatMessageCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SaveChatMessageCommandHandler> _logger;

        public SaveChatMessageCommandHandler(ApplicationDbContext context, ILogger<SaveChatMessageCommandHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(SaveChatMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = _dbContext.Chats.FirstOrDefault(c => c.Id == request.ChatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {request.ChatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var chatMessage = new ChatMessage
            {
                ChatId = request.ChatId,
                Content = request.Message.Content,
                IsFromAi = request.Message.IsFromAi,
                Author = request.Message.Author,
                Rating = Data.Enums.RatingType.None
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
            return Unit.Value;
        }
    }
}
