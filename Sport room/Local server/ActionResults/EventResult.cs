using Local_server.Models;
using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResults
{
    internal class EventResult : ActionResult
    {
        private static readonly Template _template;

        static EventResult()
        {
            var data = File.ReadAllText(StringConstants.EventsTemplatePath);
            _template = Template.Parse(data);
        }

        public EventResult(Dictionary<string, List<Event>> events, bool isAuthenticated)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = Encoding.UTF8.GetBytes(
                _template.Render(new { events = events, authenticated = isAuthenticated }));
        }
    }
}
