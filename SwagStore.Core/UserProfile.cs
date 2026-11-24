using swagStore.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using swagStore.Business.Properties;

namespace SwagStore.Core
{ 
    /// <summary>
    /// Contains methods related to the user profile menu
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Saves new hashed password into json list
        /// </summary>
        /// <param name="hash">Represents user input string for current password turned into a hash</param>
        /// <param name="newHash">Represents user input string for new password turned into a hash</param>
        /// <param name="users">List of users</param>
        public static void ChangePassword(string hash, string newHash, List<User?> users)
        {
            if (hash != newHash)
            {
                User? currentHash = CurrentHash(hash, users);
                if (currentHash != null)
                {
                    currentHash.Hash = newHash;
                }
                JsonFiles.UserSerializer(users);
            }
        }

        /// <summary>
        /// Gets user entry where hashes match for the current password
        /// </summary>
        /// <param name="hash">Represents user input string for current password turned into a hash</param>
        /// <param name="users">List of users</param>
        /// <returns></returns>
        public static User? CurrentHash(string hash, List<User?> users)
        {
            List<User?> user = users.Where(x => x?.Hash == hash).ToList();
            return user[0];
        }

        /// <summary>
        /// Adds money to user account and saves in json list
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="foundUser">Currently logged in user</param>
        /// <param name="addToBalance">User chosen amount of money to add</param>
        /// <returns></returns>
        public static double AddMoney(List<User?> users, User? foundUser, double addToBalance)
        {
            if (addToBalance > 0 && foundUser != null)
            {
                double result = addToBalance + foundUser.Balance;
                foundUser.Balance = result;

                JsonFiles.UserSerializer(users);

                return result;
            }
            return 0;
        }
    }
}
