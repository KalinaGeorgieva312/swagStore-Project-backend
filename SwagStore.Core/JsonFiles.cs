using swagStore.Business.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace swagStore.Business
{
    /// <summary>
    /// Holds initial serializations to create json files
    /// </summary>
    public class JsonFiles
    {
        /// <summary>
        /// Users list with parameters and its serialization to json
        /// </summary>
        public static void UserJson() 
        {
            List<User> users = new List<User>();

            users.Add(new User() { UserName = "Alice", Balance = 100, Hash = "5168C3A1A57E9A089C03EB45C20BCFC041D041222CD08F873F521E9CACECF429DA4F884A2167ECA8DF80EE7D772922CBDB9B31192C0ECC98EAB321AC2C87A524" });
            users.Add(new User() { UserName = "Bob", Balance = 100, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });
            users.Add(new User() { UserName = "John", Balance = 100, Hash = "8E7444A03F74BD79D47FA28E67227FAB8B3059E9B0C599F10BAEF200FAC65C17395E334E4E0523812CEA6E2F8179DCCC901F6F2EC7B097055CB0FCD25EE95495" });

            string usersFileName = "UsersList.json";
            string jsonUsersString = JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(usersFileName, jsonUsersString);
        }

        /// <summary>
        /// Products list with parameters and its serialization to json
        /// </summary>
        public static void ProductJson() 
        {
            List<Product> products = new List<Product>();

            products.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
            products.Add(new Product() { Index = "2", Brand = "Star", Type = "Jeans", Color = "blue", PriceEuro = 50 });
            products.Add(new Product() { Index = "3", Brand = "Seven", Type = "Jacke", Color = "brown", PriceEuro = 75 });

            string productsFileName = "ProductsList.json";
            string jsonProductsString = JsonSerializer.Serialize(products, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(productsFileName, jsonProductsString);
        }

        /// <summary>
        /// Method to serialize the products list after changes or additions made to it
        /// </summary>
        /// <param name="products">List of products</param>
        public static void ProductSerializer(List<Product?> products)
        {
            string fileName = "ProductsList.json";
            string jsonString = JsonSerializer.Serialize(products, new JsonSerializerOptions() { WriteIndented = true }); //serialization to json
            File.WriteAllText(fileName, jsonString);
        }

        public static List<Product> ProductDeserializer()
        {
            string fileName = "ProductsList.json";
            string jsonString = File.ReadAllText(fileName);
            List<Product?>? products = JsonSerializer.Deserialize<List<Product?>>(jsonString);

            return products;
        }

        /// <summary>
        /// Method to serialize the users list after changes or additions made to it
        /// </summary>
        /// <param name="users">List of users</param>
        public static void UserSerializer(List<User?> users)
        {
            string usersFileName = "UsersList.json";
            string jsonUsersString = JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(usersFileName, jsonUsersString);
        }

        public static List<User> UserDeserializer()
        {
            string usersFileName = "UsersList.json";
            string jsonUsersString = File.ReadAllText(usersFileName);
            List<User?>? users = JsonSerializer.Deserialize<List<User?>>(jsonUsersString);

            return users;
        }

        public static List<Brands> BrandsDeserializer()
        {
            string fileName = "BrandsList.json";
            string jsonString = File.ReadAllText(fileName);
            List<Brands> brandList = JsonSerializer.Deserialize<List<Brands?>>(jsonString);

            return brandList;
        }

        public static List<Brands> BrandsSerializer(List<Brands> brandsList)
        {
            string name = "BrandsList.json";
            string jsonS = JsonSerializer.Serialize(brandsList, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(name, jsonS);

            return brandsList;
        }
    }
}
