namespace chiai.Server.Data
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty;
        public List<Chat> Chats { get; set; } = new();
    }
}
