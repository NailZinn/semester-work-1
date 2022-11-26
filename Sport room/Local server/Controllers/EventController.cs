using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.DB;
using Local_server.Models;

namespace Local_server.Controllers
{
    [ApiController("/")]
    internal class EventController
    {
        private readonly EventDBContext _dbContext;

        public EventController()
        {
            _dbContext = new EventDBContext();
        }

        [HttpGet("^$")]
        [AuthCookieRequired]
        public async Task<EventResult> GetEventsAsync(int accountId) => await GetEventsByTypeAsync("football", accountId);

        [HttpGet("^/(hockey|tennis|basketball|volleyball)$")]
        [AuthCookieRequired]
        public async Task<EventResult> GetEventsByTypeAsync(string eventType, int accountId)
        {
            var events = await GetEventsAsync(eventType);

            return new EventResult(events, accountId != 0);
        }

        private async Task<Dictionary<string, List<Event>>> GetEventsAsync(string eventType)
        {
            var events = await _dbContext.SelectAsync(eventType);

            return events
                .Where(e => e.Date.Date == new DateTime(2022, 11, 26))
                .GroupBy(e => e.Name)
                .ToDictionary(group => group.Key, group => group.Select(e => e).ToList());
        }
    }
}
