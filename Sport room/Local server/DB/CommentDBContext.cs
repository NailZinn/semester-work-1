using Local_server.Models;
using System.Data.SqlClient;

namespace Local_server.DB
{
    internal class CommentDBContext : DBContext
    {
        public CommentDBContext() : base(StringConstants.ConnectionString)
        {
        }

        public async Task InsertAsync(DateTime date, string content, int accountId, int postId)
        {
            var sqlExpression = $"insert into Comments values('{date.ToString("u")[..^1]}', '{content}', '{accountId}', {postId})";
            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<Comment>> SelectAsync(int postId)
        {
            var result = new List<Comment>();

            var sqlExpression =
                @$"select c.Id, c.Date, Login, c.Content
                from Comments as c
                join Accounts as a
                on c.AccountId = a.Id
                where PostId = '{postId}'
                order by c.Id desc";

            using var connection = new SqlConnection(ConnectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sqlExpression, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Comment()
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        Author = (string)reader["Login"],
                        Content = (string)reader["Content"],
                        PostId = postId
                    });
                }
            }

            return result;
        }
    }
}
