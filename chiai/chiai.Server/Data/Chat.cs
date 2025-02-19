﻿namespace chiai.Server.Data
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<ChatMessage> Messages { get; set; } = new();

    }
}
