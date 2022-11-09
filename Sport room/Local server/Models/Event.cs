namespace Local_server.Models
{
    internal class Event
    {
        public int Id { get; }
        public DateTime Date { get; }
        public string Name { get; }

        public Event(int id, DateTime date, string name)
        {
            Id = id;
            Date = date;
            Name = name;
        }
    }
}
