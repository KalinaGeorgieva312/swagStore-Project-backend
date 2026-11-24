using swagStore.Business.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace swagStore.Business
{
    /// <summary>
    /// Handles logging in and hashing user input for password
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Wählt der User von der Liste und überprüft das Passwort
        /// </summary>
        /// <param name="users">Die Benutzerliste</param>
        /// <param name="username">Variable meant to represent user input for username</param>
        /// <param name="password">Variable meant to represent user input for password</param>
        /// <returns></returns>
        public static User LoggingIn(List<User?> users, string username, string password) //login method
        {
            List<User> findUser = users.Where(x => x?.UserName == username & x?.Hash == password).ToList()!;

            if (findUser.Count > 0)
            {
                return findUser[0];
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// Hashes a string
        /// </summary>
        /// <param name="password">Variable meant to represent user input for password</param>
        /// <returns></returns>
        public static string HashPassword(string password) //method to hash a password
        {
            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] pw = SHA3_512.Create().ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (byte b1 in pw)
                sb.Append(b1.ToString("X2"));

            string hash = sb.ToString();

            return hash;
        }
    }
}
