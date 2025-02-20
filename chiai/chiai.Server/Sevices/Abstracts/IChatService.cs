﻿using chiai.Server.Data.Dto;

namespace chiai.Server.Sevices.Abstracts
{
    public interface IChatService
    {
        Task SaveMessageAsync(int chatId, ChatMessageDto message);
        Task<ChatDto> StartNewChat(int userId);
    }
}
