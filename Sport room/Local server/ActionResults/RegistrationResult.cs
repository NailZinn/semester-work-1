using Scriban;
using System.Net;
using System.Text;

namespace Local_server.ActionResults
{
    internal class RegistrationResult : ActionResult
    {
        private readonly static Template _template;

        static RegistrationResult()
        {
            var data = File.ReadAllText(StringConstants.RegistrationTemplatePath);
            _template = Template.Parse(data);
        }

        public RegistrationResult(bool isSuccess)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "text/html";
            Buffer = Encoding.UTF8.GetBytes(
                _template.Render(new { success = isSuccess }));
        }

        public RegistrationResult()
        {
            StatusCode = HttpStatusCode.Redirect;
            RedirectPath = StringConstants.LoginRawUrl;
        }
    }
}
