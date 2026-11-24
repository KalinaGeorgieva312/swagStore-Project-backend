using swagStore.Business;
using swagStore.Business.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagStore.Core
{
    /// <summary>
    /// Contains Methods related to adding & saving a new user & product into their json lists
    /// </summary>
    public class AdminOptions
    {

        /// <summary>
        /// Returns the keys of a list turned into a dictionary for users
        /// </summary>
        /// <param name="usernameKey">A string containing the word username</param>
        /// <param name="userBalanceKey">A string containing the word balance</param>
        /// <param name="passwordKey">A string containing the word password</param>
        /// <returns></returns>
        public static Dictionary<string, string> KeysUsers(string usernameKey, string userBalanceKey, string passwordKey)
        {
            List<string> userKeys = new List<string>() { usernameKey, userBalanceKey, passwordKey };
            Dictionary<string, string> dictionaryKeys = userKeys.ToDictionary(x => x, x => x);

            return dictionaryKeys;
        }

        /// <summary>
        /// Saves new users into json list if the conditions are met
        /// </summary>
        /// <param name="adminInput">The values given for the creation of a new user</param>
        /// <param name="users">List of users</param>
        /// <param name="usernameKey">A string containing the word username</param>
        /// <param name="userBalanceKey">A string containing the word balance</param>
        /// <param name="passwordKey">A string containing the word password</param>
        /// <returns></returns>
        public static bool SaveUserList(Dictionary<string, string> adminInput, List<User?> users, string usernameKey, string userBalanceKey, string passwordKey)
        {
            string userName = adminInput[usernameKey];

            bool match = users.Exists(x => x?.UserName == userName);

            if (!match)
            {
                double userBalance = double.Parse(adminInput[userBalanceKey]);
                string passwordHash = adminInput[passwordKey];

                string hash = Authentication.HashPassword(passwordHash);

                if (userBalance >= 0 && userName != string.Empty && passwordHash != string.Empty)
                {
                    User newUser = new User()
                    {
                        UserName = userName,
                        Balance = userBalance,
                        Hash = hash
                    };
                    users.Add(newUser);
                    JsonFiles.UserSerializer(users);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the keys of a list turned into a dictionary for products
        /// </summary>
        /// <param name="brandKey">A string containing the word brand</param>
        /// <param name="priceKey">A string containing the word price</param>
        /// <param name="typeKey">A string containing the word type</param>
        /// <param name="colorKey">A string containing the word color</param>
        /// <returns></returns>
        public static Dictionary<string, string> KeysProducts(string brandKey, string priceKey, string typeKey, string colorKey)
        {
            List<string> productKeys = new List<string>() { brandKey, priceKey, typeKey, colorKey };
            Dictionary<string, string> dictionaryKeys = productKeys.ToDictionary(x => x, x => x);

            return dictionaryKeys;
        }

        /// <summary>
        /// Saves new products into json list if the conditions are met
        /// </summary>
        /// <param name="adminInput">The values for the creation of a new product</param>
        /// <param name="products">List of products</param>
        /// <param name="brandKey">A string containing the word brand</param>
        /// <param name="priceKey">A string containing the word price</param>
        /// <param name="typeKey">A string containing the word type</param>
        /// <param name="colorKey">A string containing the word color</param>
        /// <returns></returns>
        public static bool SaveProductsList(Dictionary<string, string> adminInput, List<Product?> products, string brandKey, string priceKey, string typeKey, string colorKey)
        {
            string productBrand = adminInput[brandKey];
            double productPriceEuro = double.Parse(adminInput[priceKey]);
            string productType = adminInput[typeKey];
            string productColor = adminInput[colorKey];

            string index = (products.Count + 1).ToString();

            if (productPriceEuro > 0 && productBrand != string.Empty && productType != string.Empty && productColor != string.Empty)
            {
                Product newProduct = new Product()
                {
                    Index = index,
                    Brand = productBrand,
                    PriceEuro = productPriceEuro,
                    Type = productType,
                    Color = productColor
                };
                products.Add(newProduct);
                JsonFiles.ProductSerializer(products);
                return true;
            }
            return false;
        }

    }
}

