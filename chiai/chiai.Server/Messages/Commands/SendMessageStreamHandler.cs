using chiai.Server.Sevices.Abstracts;
using MediatR;

namespace chiai.Server.Messages.Commands
{
    public class SendMessageStreamHandler : IRequestHandler<SendMessageStreamCommand, IAsyncEnumerable<char>>
    {
        private readonly IChatService _chatService;
        private readonly IAiChatService _aiChatService;

        public SendMessageStreamHandler(IChatService chatService, IAiChatService aiChatService)
        {
            _chatService = chatService;
            _aiChatService = aiChatService;
        }

        public async Task<IAsyncEnumerable<char>> Handle(SendMessageStreamCommand request, CancellationToken cancellationToken)
        {
            await _chatService.SaveMessageAsync(request.chatId, request.Message);
            return _aiChatService.GenerateResponseStreamAsync(request.Message.Content);
        }
    }
}
