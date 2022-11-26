using Local_server.Models;
using System.Data.SqlClient;

namespace Local_server.DB
{
    internal class SessionDBContext : DBContext
    {
        public SessionDBContext() : base(StringConstants.ConnectionString)
        {
        }

        public void Insert(Guid guid, Account account)
        {
            var sqlExpression = $"insert into Sessions values('{guid}', {account.Id}, '{account.Login}', '{account.Password}')";
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            if (!IsSessionExists(account, connection))
            {
                using var command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            else
            {
                Update(guid, account, connection);
            }
        }

        public Session? Select(string id)
        {
            var sqlExpression =
                "select top 1 * from Sessions " +
                $"where Id = '{id}'";
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Session()
                    {
                        AccountId = (int)reader["AccountId"],
                    };
                }
            };

            return null;
        }

        private bool IsSessionExists(Account account, SqlConnection connection)
        {
            var sqlExpression =
                "select top 1 * from Sessions " +
                $"where Login = '{account.Login}' or Password = '{account.Password}'";

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = command.ExecuteReader();

            return reader.HasRows;
        }

        private void Update(Guid guid, Account account, SqlConnection connection)
        {
            var sqlExpression = $"update Sessions set Id = '{guid}' where Login = '{account.Login}' and Password = '{account.Password}'";
            using var command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
        }

        public async Task UpdateSessionData(string oldLogin, string newLogin, string password, int id)
        {
            if (newLogin == password && newLogin == string.Empty)
                return;

            var updateLogin = "";
            var updatePassword = "";

            if (newLogin != "")
                updateLogin = $"Login = '{newLogin}'";
            if (password != "")
                updatePassword = $"Password = '{password}'";

            var toUpdate = new[] { updateLogin, updatePassword }
                .Where(item => item != "");

            var sqlExpression = $"update Sessions set {string.Join(", ", toUpdate)} where Login = '{oldLogin}' and AccountId = '{id}'";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
