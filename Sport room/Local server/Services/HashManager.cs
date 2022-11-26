using System.Security.Cryptography;
using System.Text;

namespace Local_server.Services
{
    internal static class HashManager
    {
        public static string CreateSalt() => Guid.NewGuid().ToString();

        public static string Encrypt(string saltedPassword)
        {
            var inputBytes = Encoding.UTF8.GetBytes(saltedPassword);
            using var sha256 = SHA256.Create();

            var outputBytes = sha256.ComputeHash(inputBytes);

            return string.Join("", outputBytes.Select(b => b.ToString()));
        }
    }
}
