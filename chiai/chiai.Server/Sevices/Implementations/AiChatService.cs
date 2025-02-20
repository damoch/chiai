using chiai.Server.Sevices.Abstracts;

namespace chiai.Server.Sevices.Implementations
{
    public class AiChatService : IAiChatService
    {
        public async IAsyncEnumerable<char> GenerateResponseStreamAsync(string userMessage)
        {
            string aiResponse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            foreach (char c in aiResponse)
            {
                yield return c; // Send one character at a time
                await Task.Delay(50); // Simulate AI typing delay
            }
        }
    }
}
