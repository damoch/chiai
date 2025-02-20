
using System.Runtime.CompilerServices;

namespace chiai.Server.Sevices.Abstracts
{
    public interface IAiChatService
    {
        IAsyncEnumerable<char> GenerateResponseStreamAsync(string userMessage, int chatId, CancellationToken cancellationToken = default);
    }
}
