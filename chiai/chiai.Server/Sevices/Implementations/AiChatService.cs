using chiai.Server.Sevices.Abstracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace chiai.Server.Sevices.Implementations
{
    public class AiChatService : IAiChatService
    {
        private readonly IChatService _chatService;
        public AiChatService(IChatService chatService)
        {
            _chatService = chatService;
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

            await _chatService.SaveMessageAsync(chatId, new ChatMessageDto { Content = sb.ToString(), IsFromAi=true, Author="ChiAI" });
        }
    }
}
