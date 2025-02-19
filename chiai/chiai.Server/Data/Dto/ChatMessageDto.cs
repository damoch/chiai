namespace chiai.Server.Data.Dto
{
    public class ChatMessageDto
    {
        public static ChatMessageDto FromChatMessage(ChatMessage chatMessage)
        {
            return new ChatMessageDto
            {
                Id = chatMessage.Id,
                Text = chatMessage.Content,
                Timestamp = chatMessage.Timestamp,
            };
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
