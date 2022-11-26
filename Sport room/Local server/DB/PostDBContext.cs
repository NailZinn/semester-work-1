using Local_server.Models;
using System.Data.SqlClient;

namespace Local_server.DB
{
    internal class PostDBContext : DBContext
    {
        public PostDBContext() : base(StringConstants.ConnectionString)
        {
        }

        public async Task InsertAsync(DateTime date, string content, int accountId)
        {
            var sqlExpression = $"insert into Posts values('{date.ToString("u")[..^1]}', '{content}', '{accountId}')";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<Post>> SelectAsync(int pageNumber, int postsPerPage)
        {
            var result = new List<Post>();

            var sqlExpression =
                @$"select top {pageNumber * postsPerPage + postsPerPage} p.Id, Date, Login, Content
                from Posts as p
                join Accounts as a
                on p.AccountId = a.Id
                except
                select top {pageNumber * postsPerPage} p.Id, Date, Login, Content
                from Posts as p
                join Accounts as a
                on p.AccountId = a.Id
                order by p.Id desc";

            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Post()
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        Author = (string)reader["Login"],
                        Content = (string)reader["Content"],
                        Url = $"/posts/{(int)reader["Id"]}"
                    });
                }
            }

            return result;
        }

        public async Task<Post?> SelectAsync(string fieldName, string fieldValue)
        {
            var sqlExpression = $"select p.Id, Date, Login, Content from Posts as p join Accounts as a on p.AccountId = a.Id where p.{fieldName} = '{fieldValue}'";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Post()
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        Author = (string)reader["Login"],
                        Content = (string)reader["Content"],
                        Url = $"/posts/{(int)reader["Id"]}"
                    };
                }
            }

            return null;
        }
    }
}
