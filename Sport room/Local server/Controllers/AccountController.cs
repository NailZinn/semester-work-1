using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.DB;
using Local_server.Services;

namespace Local_server.Controllers
{
    [ApiController("/account")]
    internal class AccountController
    {
        private readonly AccountDBContext _dbContext;

        public AccountController()
        {
            _dbContext = new AccountDBContext();
        }

        [HttpGet("^$")]
        public async Task<AccountResult> GetAccountSettingsAsync()
        {
            return await Task.Run(() => new AccountResult(false, false, false, false));
        }

        [HttpPost("^$")]
        [AuthCookieRequired]
        public async Task<AccountResult> SendAccountChanges(string newLogin, string newEmail, string oldPassword, string newPassword, int id)
        {
            if (newLogin == "" && newEmail == "" && newPassword == "")
                return new AccountResult(false, false, false, false);

            var account = await _dbContext.SelectAsync("Id", id.ToString());
            var manager = new SessionManager();

            if (account is null)
            {
                // всё плохо
            }

            if (HashManager.Encrypt(oldPassword + account!.Salt) != account.Password)
            {
                if (oldPassword == "" && newPassword != "")
                {
                    return new AccountResult(false, false, true, false);
                }
                else if (oldPassword != "" && newPassword != "")
                {
                    return new AccountResult(false, true, false, false);
                }
            }

            if (newPassword != "")
            {
                var encryptedPass = HashManager.Encrypt(newPassword + account!.Salt);
                var isSuccess = await _dbContext.UpdateAsync(newLogin, newEmail, encryptedPass, id);

                if (isSuccess)
                {
                    await manager.UpdateSession(account.Login, newLogin, encryptedPass, id);
                    return new AccountResult(false, false, false, isSuccess);
                }

                return new AccountResult(true, false, false, isSuccess);
            }
            else
            {
                var isSuccess = await _dbContext.UpdateAsync(newLogin, newEmail, newPassword, id);

                if (isSuccess)
                {
                    await manager.UpdateSession(account.Login, newLogin, newPassword, id);
                    return new AccountResult(false, false, false, isSuccess);
                }

                return new AccountResult(true, false, false, isSuccess);
            }
        }
    }
}
