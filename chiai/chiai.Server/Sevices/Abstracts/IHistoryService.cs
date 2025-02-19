using chiai.Server.Data.Dto;

namespace chiai.Server.Sevices.Abstracts
{
    public interface IHistoryService
    {
        List<ChatDto> GetHistory(int userId);

        List<ChatMessageDto> GetChatMessages(int chatId);
    }
}
