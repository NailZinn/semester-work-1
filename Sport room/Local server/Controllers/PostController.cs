using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.DB;

namespace Local_server.Controllers
{
    [ApiController("/posts")]
    internal class PostController
    {
        private const int _postsPerPage = 6;
        private readonly PostDBContext _postDBContext;
        private readonly CommentDBContext _commentDBContext;

        public PostController()
        {
            _postDBContext = new PostDBContext();
            _commentDBContext = new CommentDBContext();
        }

        [HttpGet("^$")]
        [AuthCookieRequired]
        public async Task<PostResult> GetPostsAsync(int accountId) => await GetPostsAsync(0, accountId);

        [HttpGet("^/[1-9][0-9]*$")]
        [AuthCookieRequired]
        public async Task<PostResult> GetPostAsync(int id, int accountId)
        {
            var post = await _postDBContext.SelectAsync("Id", id.ToString());
            var comments = await _commentDBContext.SelectAsync(id);

            return new PostResult(post, comments, accountId != 0);
        }

        [HttpPost("^$")]
        [AuthCookieRequired]
        public async Task<PostResult> SendPostAsync(string content, int accountId)
        {
            if (accountId == 0)
                return await GetPostsAsync(accountId);

            await _postDBContext.InsertAsync(DateTime.Now, content, accountId);

            return await GetPostsAsync(0, accountId);
        }

        [HttpPost("^/[1-9][0-9]*$")]
        [AuthCookieRequired]
        public async Task<PostResult> SendCommentAsync(int postId, string content, int accountId)
        {
            if (accountId == 0)
                return await GetPostAsync(postId, accountId);

            await _commentDBContext.InsertAsync(DateTime.Now, content, accountId, postId);

            return await GetPostAsync(postId, accountId);
        }

        [HttpGet("^/pages/[1-9][0-9]*$")]
        [Pagination]
        [AuthCookieRequired]
        public async Task<PostResult> GetPostsAsync(int pageNumber, int accountId)
        {
            var posts = await _postDBContext.SelectAsync(pageNumber, _postsPerPage);

            return pageNumber == 0
                ? new PostResult(posts, StringConstants.PostsTemplatePath, accountId != 0)
                : new PostResult(posts, StringConstants.PostTemplatePath, accountId != 0);
        }
    }
}
