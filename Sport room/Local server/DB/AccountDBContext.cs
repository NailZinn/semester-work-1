using Local_server.Models;
using System.Data.SqlClient;

namespace Local_server.DB
{
    internal class AccountDBContext : DBContext
    {
        public AccountDBContext() : base(StringConstants.ConnectionString)
        {
        }

        public async Task<bool> InsertAsync(string login, string email, string password, string salt)
        {
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            if (!await IsFieldExists(login, email, connection))
            {
                var sqlExpression = $"insert into Accounts values('{login}', '{email}', '{password}', '{salt}')";
                using var command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> IsFieldExists(string login, string email, SqlConnection connection)
        {
            var sqlExpression =
                "select top 1 * from Accounts " +
                $"where Login = '{login}' or Email = '{email}'";

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = await command.ExecuteReaderAsync();

            return reader.HasRows;
        }

        public async Task<Account?> SelectAsync(string fieldName, string fieldValue)
        {
            var sqlExpression = $"select top 1 * from Accounts where {fieldName} = '{fieldValue}'";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Account()
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                        Salt = (string)reader["Salt"]
                    };
                }
            }

            return null;
        }

        public async Task<bool> UpdateAsync(string login, string email, string password, int id)
        {
            var updateLogin = "";
            var updateEmail = "";
            var updatePassword = "";

            if (login != "")
                updateLogin = $"Login = '{login}'";
            if (email != "")
                updateEmail = $"Email = '{email}'";
            if (password != "")
                updatePassword = $"Password = '{password}'";

            var toUpdate = new[] { updateLogin, updateEmail, updatePassword }
                .Where(item => item != "");

            var sqlExpression = $"update Accounts set {string.Join(", ", toUpdate)} where Id = '{id}'";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            if (!await IsFieldExists(login, email, connection))
            {
                using var command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
