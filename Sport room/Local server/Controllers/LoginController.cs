using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.DB;
using Local_server.Services;

namespace Local_server.Controllers
{
    [ApiController("/login")]
    internal class LoginController
    {
        private readonly AccountDBContext _dbContext;

        public LoginController()
        {
            _dbContext = new AccountDBContext();
        }

        [HttpGet("^$")]
        public async Task<LoginResult> GetLoginPage()
        {
            return await Task.Run(() => new LoginResult(false, false));
        }

        [HttpPost("^$")]
        public async Task<LoginResult> PostLoginData(string login, string password, string rememberMe)
        {
            var account = await _dbContext.SelectAsync("Login", login);

            if (account is null)
            {
                return new LoginResult(true, false);
            }

            if (HashManager.Encrypt(password + account!.Salt) == account.Password)
                return new LoginResult(account!, rememberMe);

            return new LoginResult(false, true);
        }
    }
}
