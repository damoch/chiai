using chiai.Server.Data.Enums;

namespace chiai.Server.Data
{
    public class ChatMessage
    {
        public string Author { get; set; }
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsFromAi { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public RatingType Rating { get; set; }
    }
}
