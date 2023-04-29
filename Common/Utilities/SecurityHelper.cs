using System.Security.Cryptography;
using System.Text;

namespace Common.Utilities
{
    public class SecurityHelper
    {
        public static string HashPasswordSHA256(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(password);
                var bytehash = sha256.ComputeHash(byteValue);
                return Convert.ToBase64String(bytehash);
            }
        }
    }
}
