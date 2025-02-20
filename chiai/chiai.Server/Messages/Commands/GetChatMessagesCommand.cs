using chiai.Server.Data;
using MediatR;

namespace chiai.Server.Messages.Commands
{
    public record GetChatMessagesCommand(int chatId) : IRequest<IEnumerable<ChatMessageDto>>;

    public class GetChatMessagesCommandHandler : IRequestHandler<GetChatMessagesCommand, IEnumerable<ChatMessageDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GetChatMessagesCommandHandler> _logger;
        public GetChatMessagesCommandHandler(ApplicationDbContext dbContext, ILogger<GetChatMessagesCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<ChatMessageDto>> Handle(GetChatMessagesCommand request, CancellationToken cancellationToken)
        {
            var chat = _dbContext.Chats.FirstOrDefault(c => c.Id == request.chatId);
            if (chat == null)
            {
                _logger.LogError($"Chat with id {request.chatId} not found");
                throw new ArgumentException("Chat not found");
            }
            var messages = _dbContext.ChatMessages.Where(m => m.ChatId == request.chatId).ToList();
            return messages.Select(ChatMessageDto.FromChatMessage).ToList();
        }
    }
}
