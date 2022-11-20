using System.Net;

namespace Local_server.ActionResult
{
    internal class LoginResult : IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public Task<byte[]> Buffer { get; }

        private readonly static string _path = "./static/html/login.html";

        public LoginResult()
        {
            HttpStatusCode = HttpStatusCode.OK; // подумать
            ContentType = "text/html";
            Buffer = File.ReadAllBytesAsync(_path);
        }

        public LoginResult(string login, string password)
        {
            HttpStatusCode = HttpStatusCode.Redirect;
        }
    }
}
