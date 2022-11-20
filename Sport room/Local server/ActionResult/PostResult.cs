using Local_server.Models;
using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResult
{
    internal class PostResult : IActionResult
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ContentType { get; }
        public Task<byte[]> Buffer { get; }

        public PostResult(Post? post)
        {
            HttpStatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            // дореализовать
        }

        public PostResult(Post[] posts, string path)
        {
            HttpStatusCode = HttpStatusCode.OK;
            ContentType = "text/html";

            var data = File.ReadAllText(path);
            var template = Template.Parse(data);
            var htmlPage = template.Render(new { posts = posts });

            Buffer = Task.Run(() => Encoding.UTF8.GetBytes(htmlPage));
        }
    }
}
