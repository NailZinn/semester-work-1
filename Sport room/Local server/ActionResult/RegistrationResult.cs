using System.Net;

namespace Local_server.ActionResult
{
    internal class RegistrationResult : IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public Task<byte[]> Buffer { get; }

        private readonly static string _path = "./static/html/registration.html";

        public RegistrationResult()
        {
            HttpStatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = File.ReadAllBytesAsync(_path);
        }

        public RegistrationResult(string login, string email, string phone, string password)
        {
            HttpStatusCode = HttpStatusCode.Redirect;
        }
    }
}
