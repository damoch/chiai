using chiai.Server.Sevices.Abstracts;

namespace chiai.Server.Sevices.Implementations
{
    public class AiChatService : IAiChatService
    {
        private readonly IChatService _chatService;
        public AiChatService(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async IAsyncEnumerable<char> GenerateResponseStreamAsync(string userMessage, int chatId)
        {
            string aiResponse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            foreach (char c in aiResponse)
            {
                yield return c;
                await Task.Delay(50);
            }

            await _chatService.SaveMessageAsync(chatId, new ChatMessageDto { Content = aiResponse, IsFromAi=true, Author="ChiAI" });
        }
    }
}
