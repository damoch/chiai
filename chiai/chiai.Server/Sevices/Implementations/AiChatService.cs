using chiai.Server.Messages.Commands;
using chiai.Server.Sevices.Abstracts;
using MediatR;
using System.Runtime.CompilerServices;
using System.Text;

namespace chiai.Server.Sevices.Implementations
{
    public class AiChatService : IAiChatService
    {
        private readonly IMediator _mediator;
        public AiChatService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async IAsyncEnumerable<char> GenerateResponseStreamAsync(string userMessage, int chatId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string aiResponse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            var sb = new StringBuilder();

            foreach (char c in aiResponse)
            {
                yield return c;
                sb.Append(c);
                await Task.Delay(50);
                if(cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
            await _mediator.Send(new SaveChatMessageCommand(chatId, new ChatMessageDto { Content = sb.ToString(), IsFromAi = true, Author = "ChiAI" }));
        }
    }
}
