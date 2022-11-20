using Local_server.Models;
using System.Net;

namespace Local_server.ActionResult
{
    internal class EventResult : IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public Task<byte[]> Buffer { get; }

        public EventResult(Dictionary<string, List<Event>> events)
        {

        }

        public EventResult(Event? @event)
        {

        }
    }
}
