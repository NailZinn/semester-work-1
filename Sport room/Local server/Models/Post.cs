namespace Local_server.Models
{
    internal class Post
    {
        public int Id { get; }
        public string Author { get; }
        public DateTime Date { get; }
        public int UserId { get; }
        public string Content { get; }

        public Post(int id, string author, DateTime date, int userId, string content)
        {
            Id = id;
            Author = author;
            Date = date;
            UserId = userId;
            Content = content;
        }
    }
}
