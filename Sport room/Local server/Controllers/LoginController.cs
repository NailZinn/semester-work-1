using Local_server.ActionResult;
using Local_server.Attributes;

namespace Local_server.Controllers
{
    [ApiController("/login")]
    internal class LoginController
    {
        [HttpGet("^$")]
        public LoginResult GetLoginPage()
        {
            return new LoginResult();
        }

        [HttpPost("^$")]
        public LoginResult Post(string login, string password)
        {
            return new LoginResult(login, password);
        }
    }
}
