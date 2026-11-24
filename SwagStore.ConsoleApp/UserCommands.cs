using swagStore.Business;
using swagStore.Business.Properties;
using swagStore.ConsoleApp;
using SwagStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwagStore.ConsoleApp
{
    /// <summary>
    /// Methods that work after specific user commands
    /// </summary>
    public class UserCommands
    {
        /// <summary>
        /// Method for when the user exits the program
        /// </summary>
        public static void Exit()
        {
            Console.WriteLine("\nVielen Dank für Ihren Einkauf!");
        }

        /// <summary>
        /// Calls the pay method and displays the results on the console, then clears the shopping cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart list containing products chosen by user for purchase</param>
        /// <param name="users">List of users</param>
        /// <param name="foundUser">A logged in user from the users list</param>
        public static void UserPay(List <Product?> shoppingCart, List <User?> users, User? foundUser)
        {
            double sum = Payment.Pay(shoppingCart, users, foundUser);

            if (shoppingCart.Count != 0 && foundUser?.Balance >= sum)
            {
                Console.WriteLine("\nKauf erfolgreich. Neuer Kontostand: " + foundUser.Balance);
            }

            if (foundUser?.Balance < sum)
            {
                Console.WriteLine("\nZu wenig Guthaben! Versuchen Sie es nochmal.");
            }

            shoppingCart.Clear();
        }
    }
}
