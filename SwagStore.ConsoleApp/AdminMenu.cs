using swagStore.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using swagStore.Business.Properties;
using SwagStore.Core;

namespace SwagStore.ConsoleApp
{
    /// <summary>
    /// Contains methods related to the admin menu, like saving a new user to the json file & saving a new product to a json file
    /// </summary>
    public class AdminMenu
    {

        /// <summary>
        /// Displays instructions and messages relating to saving a new user & calls methods that take user input & save it in the json file 
        /// </summary>
        /// <param name="users">List of users</param>
        public static void UserAddition(List<User?> users)
        {
            string usernameKey = "\nUser Name:";
            string userBalanceKey = "\nUser Balance:";
            string passwordKey = "\nPassword:";

            Dictionary<string, string> adminInput = AddNewObject(AdminOptions.KeysUsers(usernameKey, userBalanceKey, passwordKey));

            if (AdminOptions.SaveUserList(adminInput, users, usernameKey, userBalanceKey, passwordKey) == true)
            {
                Console.WriteLine("\nDer neue User wurde in UsersList.json gespeichert.");
            }

            else
            {
                Console.WriteLine("\nDer User Balance kann nicht negativ sein & Parameters können nicht leer sein!");
                Console.WriteLine("Sie können keinen Benutzer mit einem bereits in der Liste vorhandenen Namen hinzufügen.");
            }
        }

        /// <summary>
        /// Displays instructions and messages relating to saving a new product & calls methods that take user input & save it in the json file
        /// </summary>
        /// <param name="products">List of products</param>
        public static void ProductAddition(List<Product?> products)
        {
            string brandKey = "\nBrand:";
            string priceKey = "\nPrice Euro:";
            string typeKey = "\nType:";
            string colorKey = "\nColor:";

            Dictionary<string, string> adminInput = AddNewObject(AdminOptions.KeysProducts(brandKey, priceKey, typeKey, colorKey));

            if (AdminOptions.SaveProductsList(adminInput, products, brandKey, priceKey, typeKey, colorKey) == true)
            {
                Console.WriteLine("\nDas neue Product wurde in ProductsList.json gespeichert.");
            }

            else
            {
                Console.WriteLine("\nDer Preis kann nicht 0 oder negativ sein & Parameters können nicht leer sein!");
            }
        }

        /// <summary>
        /// Admin Menu where the admin chooses whether to add a new user or a new product
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="products">List of products</param>
        public static void AdminDisplay(List<User?> users, List<Product?> products)
        {
            Console.WriteLine("\nWillkommen, Admin! Wählen Sie 1, um einen neuen User hinzufügen, oder wählen Sie 2, um ein neues Produkt hinzufügen.");
            string adminChoice = Helper.UserInput();

            switch (adminChoice)
            {
                case "1":
                    UserAddition(users);
                    break;
                case "2":
                    ProductAddition(products);
                    break;
                default:
                    Console.WriteLine("\nFalsche Eingabe!");
                    break;
            }
        }

        /// <summary>
        /// Prints the keys in the console & takes user input for the values
        /// </summary>
        /// <param name="keyValues">A dictionary of keys</param>
        /// <returns></returns>
        public static Dictionary<string, string> AddNewObject(Dictionary<string, string> keyValues)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in keyValues)
            {
                Console.WriteLine(item.Key);

                string adminInput = Helper.UserInput() ?? "";
                result.Add(item.Key, adminInput);
            }
            return result;
        }
    }
}
