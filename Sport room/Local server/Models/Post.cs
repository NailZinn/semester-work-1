namespace Local_server.Models
{
    internal class Post
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int AccountId { get; set; }
        public string Url { get; set; }
    }
}
