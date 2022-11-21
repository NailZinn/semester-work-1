namespace Local_server.DB
{
    internal abstract class DBContext
    {
        protected string ConnectionString { get; }

        protected DBContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
