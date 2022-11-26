using System.Net;

namespace Local_server.ActionResults
{
    internal abstract class ActionResult
    {
        public HttpStatusCode StatusCode { get; protected set; }
        public string? ContentType { get; protected set; }
        public byte[] Buffer { get; protected set; }
        public CookieCollection Cookies { get; protected set; }
        public string? RedirectPath { get; protected set; }
    }
}
