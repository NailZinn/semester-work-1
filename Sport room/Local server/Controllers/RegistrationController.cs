using Local_server.ActionResult;
using Local_server.Attributes;

namespace Local_server.Controllers
{
    [ApiController("/registration")]
    internal class RegistrationController
    {
        [HttpGet("^$")]
        public RegistrationResult GetRegistrationPage()
        {
            return new RegistrationResult();
        }

        [HttpPost("^$")]
        public RegistrationResult Post(string login, string email, string phone, string password)
        {
            return new RegistrationResult(login, email, phone, password);
        }
    }
}
