using Local_server.DB;
using Local_server.Models;

namespace Local_server.Services
{
    internal class SessionManager
    {
        private readonly SessionDBContext _dbContext;

        public SessionManager()
        {
            _dbContext = new SessionDBContext();
        }

        public int GetAccountIdFromSession(string id)
        {
            var session = _dbContext.Select(id);

            return session is not null
                ? session.AccountId
                : 0;
        }

        public void AddSession(Guid guid, Account account) => _dbContext.Insert(guid, account);

        public async Task UpdateSession(string oldLogin, string newLogin, string newPassword, int accountId)
        {
            await _dbContext.UpdateSessionData(oldLogin, newLogin, newPassword, accountId);
        }
    }
}
