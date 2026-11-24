using swagStore.Business;
using swagStore.Business.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SwagStore.ConsoleApp
{
    /// <summary>
    /// Holds methods that display on the console
    /// </summary>
    public class DisplayProducts
    {
        /// <summary>
        /// Prints all products in the products list on the console
        /// </summary>
        /// <param name="products">List of products</param>
        public static void PrintProducts(List<Product?> products)
        {
            Console.WriteLine("\nHier sind alle unsere Products. Bitte schreiben Sie den Artikel Index, um den Artikel in Ihren Warenkorb zu legen.");
            Console.WriteLine("Schreiben Sie 'bezahlen', um die gewünschte Artikels zu kaufen. Um den Einkauf zu beenden, schreiben Sie 'exit'.\n");

            Display(products);
        }

        /// <summary>
        /// Displays the products in the shopping cart in the console
        /// </summary>
        /// <param name="shoppingCart">Shopping cart list containing products chosen by user for purchase</param>
        public static void ProductChoice(List<Product?> shoppingCart) //method for user choice msg in shopping cart
        {
            Console.WriteLine("\nProdukte in Warenkorb:");
            Display(shoppingCart);
        }

        /// <summary>
        /// Formula for how products should be displayed in the console
        /// </summary>
        /// <param name="list">Placeholder for a list of type Product</param>
        public static void Display(List<Product?> list)
        {
            foreach (Product? item in list)
            {
                Console.WriteLine("Index: " + item?.Index + " " + "Brand: " + item?.Brand + " " + "Type: " + item?.Type + " " + "Color: " + item?.Color + " " + "Price: " + item?.PriceEuro + " Euro");
            }
        }

        /// <summary>
        /// Displays the total sum of items in the shopping cart list
        /// </summary>
        /// <param name="shoppingCart">Shopping cart list containing products chosen by user for purchase</param>
        /// <param name="totalSum">sum of all products in the shopping cart list</param>
        public static void TotalSumDisplay(List<Product?> shoppingCart, double totalSum)
        {
            if (shoppingCart.Count > 3)
            {
                Console.WriteLine("\n10% Discount");
            }

            Console.WriteLine("\nGesamtbetrag: " + totalSum);
        }
    }
}
