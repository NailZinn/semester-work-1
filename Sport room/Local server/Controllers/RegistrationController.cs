using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.DB;
using Local_server.Services;

namespace Local_server.Controllers
{
    [ApiController("/registration")]
    internal class RegistrationController
    {
        private readonly AccountDBContext _dbContext;

        public RegistrationController()
        {
            _dbContext = new AccountDBContext();
        }

        [HttpGet("^$")]
        public async Task<RegistrationResult> GetRegistrationPage()
        {
            return await Task.Run(() => new RegistrationResult(true));
        }

        [HttpPost("^$")]
        public async Task<RegistrationResult> PostRegistrationData(string login, string email, string password)
        {
            var salt = HashManager.CreateSalt();
            var encyptedPassword = HashManager.Encrypt(password + salt);

            var isSuccess = await _dbContext.InsertAsync(login, email, encyptedPassword, salt);

            return isSuccess
                ? new RegistrationResult()
                : new RegistrationResult(isSuccess);
        }
    }
}
