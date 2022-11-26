using Local_server.Models;
using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResults
{
    internal class PostResult : ActionResult
    {
        private static readonly Template _commentsTemplate;

        static PostResult()
        {
            var data = File.ReadAllText(StringConstants.CommentsTemplatePath);
            _commentsTemplate = Template.Parse(data);
        }

        public PostResult(Post? post, List<Comment> comments, bool isAuthenticated)
        {
            if (post is null)
            {
                StatusCode = HttpStatusCode.NotFound;
                ContentType = "text/html";
                Buffer = File.ReadAllBytes(StringConstants.ErrorPagePath);
            }
            else
            {
                StatusCode = HttpStatusCode.OK;
                ContentType = "text/html";
                Buffer = Encoding.UTF8.GetBytes(
                    _commentsTemplate.Render(new
                        { post = post, comments = comments, authenticated = isAuthenticated }));
            }
        }

        public PostResult(List<Post> posts, string path, bool isAuthenticated)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "text/html";

            var data = File.ReadAllText(path);
            var template = Template.Parse(data);
            var htmlPage = template.Render(new { posts = posts, authenticated = isAuthenticated });

            Buffer = Encoding.UTF8.GetBytes(htmlPage);
        }
    }
}
