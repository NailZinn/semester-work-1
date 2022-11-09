namespace Local_server.Models
{
    internal class Comment
    {
        public int Id { get; }
        public DateTime Date { get; }
        public int UserId { get; }
        public string Content { get; }

        public Comment(int id, DateTime date, int userId, string content)
        {
            Id = id;
            Date = date;
            UserId = userId;
            Content = content;
        }
    }
}
