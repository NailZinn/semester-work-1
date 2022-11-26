using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResults
{
    internal class AccountResult : ActionResult
    {
        private static readonly Template _template;

        static AccountResult()
        {
            var data = File.ReadAllText(StringConstants.AccountSettingsPath);
            _template = Template.Parse(data);
        }

        public AccountResult(bool isLoginOrEmailExists, bool isWrongPassword, bool isEmptyassword, bool isSuccess)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = Encoding.UTF8.GetBytes(
                _template.Render(new
                {
                    loginOrEmailExists = isLoginOrEmailExists, 
                    wrongPassword = isWrongPassword, 
                    emptyPassword = isEmptyassword,
                    success = isSuccess
                }));
        }
    }
}
