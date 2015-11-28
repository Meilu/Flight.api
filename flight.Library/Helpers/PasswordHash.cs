using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace flight.Library.Helpers
{
    public class PasswordHash
    {
        private const int HashIterations = 50;

        public static string CreateSalt(string userName, string userSalt)
        {

            var hasher = new Rfc2898DeriveBytes(userName,
                System.Text.Encoding.UTF8.GetBytes(userSalt), HashIterations);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }


        public static string HashPassword(string salt, string password)
        {
            var hasher = new Rfc2898DeriveBytes(password,
                System.Text.Encoding.UTF8.GetBytes(salt), HashIterations);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }


    }
}
