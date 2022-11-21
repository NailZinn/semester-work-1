using System.Net;

namespace Local_server.ActionResult
{
    internal interface IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public byte[] Buffer { get; }
    }
}
