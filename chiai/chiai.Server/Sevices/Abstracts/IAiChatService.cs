
namespace chiai.Server.Sevices.Abstracts
{
    public interface IAiChatService
    {
        IAsyncEnumerable<char> GenerateResponseStreamAsync(string userMessage);
    }
}
