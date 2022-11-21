using Local_server.Models;
using System.Data.SqlClient;

namespace Local_server.DB
{
    internal class EventDBContext : DBContext
    {
        public EventDBContext() :
            base(StringConstants.ConnectionString)
        {

        }

        public async Task<List<Event>> Select(string eventType)
        {
            var result = new List<Event>();

            var sqlExpression = $"select * from Events where EventType = '{eventType}'";
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            var command = new SqlCommand(sqlExpression, connection);
            var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Event()
                    {
                        Id = (int)reader["Id"],
                        Date = (DateTime)reader["Date"],
                        Name = (string)reader["Name"],
                        EventType = eventType,
                        TeamA = (string)reader["TeamA"],
                        TeamB = (string)reader["TeamB"]
                    });
                }
            }

            return result;
        }
    }
}
