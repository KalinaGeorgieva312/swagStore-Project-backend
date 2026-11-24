using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swagStore.Business.Properties
{
    /// <summary>
    /// Parameters for the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// The name of the user
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Property showing whether the user is an admin or not
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// How much money there is in the user account
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Hashed passwords
        /// </summary>

        public string? Hash { get; set; }
    }
}
