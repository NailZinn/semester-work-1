using Local_server.Models;
using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResult
{
    internal class EventResult : IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public byte[] Buffer { get; }

        private static readonly Template _template;

        static EventResult()
        {
            var data = File.ReadAllText(StringConstants.EventsTemplatePath);
            _template = Template.Parse(data);
        }

        public EventResult(Dictionary<string, List<Event>> events)
        {
            HttpStatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = Encoding.UTF8.GetBytes(
                _template.Render(new { events = events }));
        }
    }
}
