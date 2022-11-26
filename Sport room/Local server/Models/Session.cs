namespace Local_server.Models
{
    internal class Session
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
