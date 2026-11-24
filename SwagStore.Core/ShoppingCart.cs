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
    /// Holds shopping cart related methods - finding a user chosen product in the products list & sum of all products in the shopping cart
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// Calculates the sum of all products in the shopping cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart list containing products chosen by user for purchase</param>
        /// <returns></returns>
        public static double ShoppingCartSum(List<Product?> shoppingCart)
        {
            double totalSum = 0;

            foreach (Product? item in shoppingCart)
            {
                if (item != null)
                {
                    totalSum += item.PriceEuro;
                }
            }

            if (shoppingCart.Count > 3)
            {
                double sum = totalSum * 0.9;
                return sum;
            }

            else
            {
                return totalSum;
            }
        }

        /// <summary>
        /// Finds the product chosen by the user in the products list
        /// </summary>
        /// <param name="productChoice">Variable representing user input for choosing a product</param>
        /// <param name="products">List of products</param>
        /// <returns></returns>
        public static Product? MatchUserInputAndList(string productChoice, List<Product?> products)
        {
            Product? selectedProduct = products.Find(x => x?.Index == productChoice);

            return selectedProduct;
        }
    }
}
