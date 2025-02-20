using chiai.Server.Data;

public class ChatMessageDto
{
    public static ChatMessageDto FromChatMessage(ChatMessage chatMessage)
    {
        return new ChatMessageDto
        {
            Id = chatMessage.Id,
            Content = chatMessage.Content,
            Timestamp = chatMessage.Timestamp,
            IsFromAi = chatMessage.IsFromAi,
            Author = chatMessage.Author,
            Rating = (int)chatMessage.Rating//this is a nullable bool

        };
    }
    public int Rating { get; set; }
    public string Author { get; set; }
    public bool IsFromAi { get; set; }
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
