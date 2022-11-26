namespace Local_server.Models
{
    internal class Event
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string EventType { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
    }
}
