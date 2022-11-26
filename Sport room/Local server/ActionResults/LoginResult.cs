using Local_server.Models;
using Local_server.Services;
using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResults
{
    internal class LoginResult : ActionResult
    {
        private static readonly Template _template;

        static LoginResult()
        {
            var data = File.ReadAllText(StringConstants.LoginTemplatePath);
            _template = Template.Parse(data);
        }

        public LoginResult(bool isWrongLogin, bool isWrongPassword)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = Encoding.UTF8.GetBytes(
                _template.Render(new { wrongLogin = isWrongLogin, wrongPassword = isWrongPassword }));
        }

        public LoginResult(Account account, string rememberMe)
        {
            StatusCode = HttpStatusCode.Redirect;
            RedirectPath = StringConstants.MainRawUrl;

            var sessionId = Guid.NewGuid();

            if (rememberMe != "")
            {
                var cookie = new Cookie("SessionId", sessionId.ToString());
                cookie.Expires = DateTime.Now.AddYears(1);
                Cookies = new CookieCollection() { cookie };
            }
            else
            {
                var cookie = new Cookie("SessionId", sessionId.ToString());
                cookie.Expires = DateTime.Now.AddHours(1);
                Cookies = new CookieCollection() { cookie };
            }

            var manager = new SessionManager();
            manager.AddSession(sessionId, account);
        }
    }
}
