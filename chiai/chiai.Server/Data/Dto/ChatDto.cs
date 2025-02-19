namespace chiai.Server.Data.Dto
{
    public class ChatDto
    {
        public static ChatDto FromChat(Chat chat)
        {
            return new ChatDto
            {
                Id = chat.Id,
                Title = chat.Title
            };
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
