namespace Local_server.Models
{
    internal class User
    {
        public int Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }
        public string Phone { get; }

        public User(int id, string name, string password, string email, string phone)
        {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
            Phone = phone;
        }
    }
}
