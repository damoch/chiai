using chiai.Server.Data.Dto;
using MediatR;

namespace chiai.Server.Messages.Commands
{
    public record SendMessageStreamCommand(int chatId, ChatMessageDto Message)
        : IRequest<IAsyncEnumerable<char>>;
}
