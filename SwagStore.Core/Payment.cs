using swagStore.Business;
using swagStore.Business.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwagStore.Core
{
    /// <summary>
    /// Holds payment math and users list serialization method
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Calls the shopping cart method and does the payment math 
        /// </summary>
        /// <param name="shoppingCart">Shopping cart list containing products chosen by user for purchase</param>
        /// <param name="users">List of users</param>
        /// <param name="foundUser">A logged in user from the users list</param>
        /// <returns></returns>
        public static double Pay(List<Product?> shoppingCart, List<User?> users, User? foundUser) 
        {
            double shoppingSum = ShoppingCart.ShoppingCartSum(shoppingCart);

            if(foundUser?.Balance >= shoppingSum && foundUser.Balance > 0 && shoppingSum > 0)
            {
                foundUser.Balance -= shoppingSum;
                JsonFiles.UserSerializer(users);
                return foundUser.Balance;
            }
            return shoppingSum;
        }
    }
}
