using chiai.Server.Data.Dto;

namespace chiai.Server.Sevices.Abstracts
{
    public interface IChatService
    {
        Task<ChatDto> StartNewChat(int userId);
    }
}
