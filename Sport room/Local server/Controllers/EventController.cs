using Local_server.ActionResult;
using Local_server.Attributes;
using Local_server.Models;

namespace Local_server.Controllers
{
    [ApiController("/")]
    internal class EventController
    {
        public static List<Event> _events = new()
        {
            new Event(1, DateTime.Now.AddHours(1), "Футбол", "Английская премьер-лига", "Брайтон", "Астон Вилла"),
            new Event(2, DateTime.Now.AddHours(2), "Футбол", "Английская премьер-лига", "Фулхэм", "Манчестер Юнайтед"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Майнц", "Айнтрахт"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Майнц", "Айнтрахт"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Майнц", "Айнтрахт"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Аталанта", "Интер"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Верона", "Специя"),
            new Event(3, DateTime.Now.AddHours(3), "Футбол", "Бундеслига", "Монца", "Салернитана")
        };

        [HttpGet("^$")]
        public EventResult GetEvents()
            => GetEventsBySportName("football");

        [HttpGet("^(hockey|tennis|basketball|volleyball)$")]
        public EventResult GetEventsBySportName(string name)
        {
            var events = _events
                .Where(e => e.EventType == name && e.Date.Date == DateTime.Now.Date)
                .GroupBy(e => e.Name)
                .ToDictionary(group => group.Key, group => group.Select(e => e).ToList());

            return new EventResult(events);
        }

        [HttpGet("^(football|hockey|tennis|basketball|volleyball)/[1-9][0-9]*$")]
        public EventResult GetEvent(string name, int id)
        {
            var events = _events
                .Where(e => e.EventType == name && e.Date.Date == DateTime.Now.Date)
                .GroupBy(e => e.Name)
                .ToDictionary(group => group.Key, group => group.Select(e => e).ToList());

            var @event = events.Count != 0
                ? events.Values
                    .SelectMany(x => x)
                    .First(e => e.Id == id)
                : null;

            return new EventResult(@event);
        }
    }
}
