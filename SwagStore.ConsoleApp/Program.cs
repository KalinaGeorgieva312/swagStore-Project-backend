using swagStore.Business;
using swagStore.Business.Properties;
using swagStore.ConsoleApp;
using SwagStore.ConsoleApp;
using SwagStore.Core;
using System.Runtime.CompilerServices;
using System.Text.Json;

List<Brands> brandsList = BrandGrouping.BrandList();

brandsList.Add(new Brands { Id = "015", Brand = "Aldercove" });
brandsList.Add(new Brands { Id = "016", Brand = "Running" });
brandsList.Add(new Brands { Id = "017", Brand = "Marrow" });
brandsList.Add(new Brands { Id = "018", Brand = "Sunderline" });
brandsList.Add(new Brands { Id = "019", Brand = "Kourb" });

string name = "BrandsList.json";
string jsonS = JsonSerializer.Serialize(brandsList, new JsonSerializerOptions() { WriteIndented = true });
File.WriteAllText(name, jsonS);

BrandGrouping.UpdateProducts();

JsonFiles.UserJson();
JsonFiles.ProductJson();

string usersFileName = "UsersList.json";
string jsonUsersString = File.ReadAllText(usersFileName);
List<User?> users = JsonSerializer.Deserialize<List<User?>>(jsonUsersString) ??
    throw new Exception("User List ist leer.");

if (!File.Exists(usersFileName))
{
    Console.WriteLine("\nFehler! Die Datei existiert nicht!");
}

User foundUser = null ?? new User();
bool notFound = true;

while (notFound)
{
    Console.WriteLine("Username:");
    string username = Helper.UserInput();

    Console.WriteLine("\nPassword:");
    string password = Helper.UserInput();

    string hash = Authentication.HashPassword(password);

    try
    {
        foundUser = Authentication.LoggingIn(users, username, hash);
        if (foundUser != null)
        {
            break;
        }
    }
    catch (Exception)
    {
        Console.WriteLine("\nFalsche oder Leere Anmeldedaten!\n");
    }
}

string fileName = "ProductsList.json";
string jsonString = File.ReadAllText(fileName);
List<Product?> products = JsonSerializer.Deserialize<List<Product?>>(jsonString) ??
    throw new Exception("Product List ist leer.");

if (!File.Exists(fileName))
{
    Console.WriteLine("\nFehler! Die Datei existiert nicht.");
}

if (foundUser?.UserName == "Admin")
{
    try
    {
        AdminMenu.AdminDisplay(users, products);
    }

    catch (FormatException)
    {
        Console.WriteLine("\nUser Balance & Preis kann keine Buchstaben haben oder leer sein!");
    }
}

else
{
    Console.WriteLine("\nWillkommen, " + foundUser?.UserName + "! Schreiben Sie 'einkaufen', um einzukaufen, oder schreiben Sie 'profil', um Ihr Profil zu bearbeiten.");

    string input = Helper.UserInput();

    if (input == "profil")
    {
        DisplayUserProfile.ProfileDisplay(users, foundUser);
    }

    else if (input == "einkaufen")
    {
        Console.WriteLine("\nWillkommen, " + foundUser?.UserName + "!");
        Console.WriteLine("Kontostand: " + foundUser?.Balance + " Euro");

        DisplayProducts.PrintProducts(products);

        double totalSum = 0;

        List<Product?> shoppingCart = new List<Product?>();

        while (true)
        {
            Console.WriteLine();
            string productChoice = Helper.UserInput();

            Product? selectedProduct = ShoppingCart.MatchUserInputAndList(productChoice, products);

            if (selectedProduct?.PriceEuro < 0)
                throw new Exception("\nDer Preis darf nicht negativ sein!");

            if (selectedProduct != null)
            {
                shoppingCart.Add(selectedProduct);
            }

            if (productChoice == "exit")
            {
                UserCommands.Exit();
                break;
            }

            if (shoppingCart.Count > 0 && productChoice != "bezahlen")
            {
                DisplayProducts.ProductChoice(shoppingCart);

                totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

                DisplayProducts.TotalSumDisplay(shoppingCart, totalSum);
            }

            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("\nWarenkorb ist leer!");
            }

            if (productChoice == "bezahlen" && foundUser != null)
            {
                UserCommands.UserPay(shoppingCart, users, foundUser);
            }
        }
    }

    else
    {
        Console.WriteLine("\nUngültiger Befehl!");
    }
}

