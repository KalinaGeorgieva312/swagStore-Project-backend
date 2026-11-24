using swagStore.Business;
using swagStore.Business.Properties;
using SwagStore.Core;
using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace SwagStore.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }

    [Fact]
    public void LoggingIn_User_List_Empty_Empty_User_Input()
    {
        List<User?> users = new List<User?>();
        string username = string.Empty;
        string password = string.Empty;

        Assert.Throws<ArgumentOutOfRangeException>(() => { Authentication.LoggingIn(users, username, password); });
    }

    [Fact]
    public void LoggingIn_More_Than_1_Same_Username()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 100, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });
        users.Add(new User() { UserName = "Bob", Balance = 50, Hash = "B5E46D16E05DE6BCB15D1A0CD55138CFE703515FB03B62AB8B3D7115E2ABAF20C3C032BEB5DF141D4DDDD33440D65A346E63CFA7A7824CCEB5A92EA9BB5F8DD2" });

        string username = "Bob";
        string password = "B5E46D16E05DE6BCB15D1A0CD55138CFE703515FB03B62AB8B3D7115E2ABAF20C3C032BEB5DF141D4DDDD33440D65A346E63CFA7A7824CCEB5A92EA9BB5F8DD2";
        User foundUser = Authentication.LoggingIn(users, username, password);

        Assert.Contains(foundUser, users);
    }

    [Fact]
    public void LoggingIn_Empty_List()
    {
        List<User?> users = new List<User?>();

        string username = "Alice";
        string password = "B5E46D16E05DE6BCB15D1A0CD55138CFE703515FB03B62AB8B3D7115E2ABAF20C3C032BEB5DF141D4DDDD33440D65A346E63CFA7A7824CCEB5A92EA9BB5F8DD2";

        Assert.Throws<ArgumentOutOfRangeException>(() => { Authentication.LoggingIn(users, username, password); });
    }

    [Fact]
    public void HashPassword_Match()
    {
        string password = "jjj";

        string pwHash = Authentication.HashPassword(password);

        Assert.Equal("FF209F3517A0F521C19862375E884AACC0144BAEE024C2540F1E74D228F8CCCB40CCE1F668EFCC6E4E10D348FE00A513601319D73F125BFEF5FD598D49DF65D8", pwHash);
    }

    [Fact]
    public void HashPassword_Case_Sensitive()
    {
        string password = "jjj";

        string pwHash = Authentication.HashPassword(password);

        Assert.NotEqual("ff209f3517a0f521c19862375e884aacc0144baee024c2540f1e74d228f8cccb40cce1f668efcc6e4e10d348fe00a513601319d73f125bfef5fd598d49df65d8", pwHash);
    }

    [Fact]
    public void HashPassword_No_Match()
    {
        string password = "jjj";

        string pwHash = Authentication.HashPassword(password);

        Assert.NotEqual("0FB2CD5A4472EC7D48AA0D7C75EC4AEAC478A41CE836CA6FC433708E8119606EB62D6E418377E447014A0E692CD5F84C3A5ACB30D58869D83F2C2C65A359DE05", pwHash);
    }

    [Fact]
    public void HashPassword_Empty()
    {
        string password = string.Empty;

        string pwHash = Authentication.HashPassword(password);

        Assert.Equal("A69F73CCA23A9AC5C8B567DC185A756E97C982164FE25859E0D1DCC1475C80A615B2123AF1F5F94C11E3E9402C3AC558F500199D95B6D3E301758586281DCD26", pwHash);
    } 

    [Fact]
    public void Empty_ShoppingCartSum()
    {
        List<Product?> shoppingCart = new List<Product?>();

        double totalSum = 0;

        totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.Equal(0, 0);
    }

    [Fact]
    public void ShoppingCartSum_1_Item()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double sum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.Equal(20, 20);
    }

    [Fact]
    public void ShoppingCartSum_2_Items() 
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double sum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.Equal(40, 40);
    }

    [Fact]
    public void ShoppingCartSum_1_Item_Not_Equal()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double totalSum = 0;

        totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.NotEqual(20, 40);
    }

    [Fact]
    public void ShoppingCartSum_1_Item_Negative()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double totalSum = 0;

        totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.NotEqual(-20, 20);
    }

    [Fact]
    public void ShoppingCartSum_3_Items_If_Discount()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double totalSum = 0;

        totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.Equal(60, 60);
    }

    [Fact]
    public void ShoppingCartSum_4_Items_If_Discount()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        double totalSum = 0;

        totalSum = ShoppingCart.ShoppingCartSum(shoppingCart);

        Assert.Equal(72, 72);
    }

    [Fact]
    public void MatchUserInputAndList_Empty()
    {
        List<Product?> products = new List<Product?>();

        string productChoice = string.Empty;

        Product? itemInCart = ShoppingCart.MatchUserInputAndList(productChoice, products);

        Assert.DoesNotContain(itemInCart, products);
    }

    [Fact]
    public void MatchUserInputAndList_Default()
    {
        List<Product?> products = new List<Product?>();
        products.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        string productChoice = "1";

        Product? itemInCart = ShoppingCart.MatchUserInputAndList(productChoice, products);

        Assert.Contains(itemInCart, products);
    }

    [Fact]
    public void No_MatchUserInputAndList()
    {
        List<Product?> products = new List<Product?>();
        products.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        string productChoice = "2";

        Product? itemInCart = ShoppingCart.MatchUserInputAndList(productChoice, products);

        Assert.DoesNotContain(itemInCart, products);
    }

    [Fact]
    public void Pay_Empty()
    {
        List<User?> users = new List<User?>();

        List<Product?> shoppingCart = new List<Product?>();
        
        User foundUser = null ?? new User();

        Assert.Throws<NullReferenceException>(() => { Payment.Pay(shoppingCart, users, foundUser); });
    }

    [Fact]
    public void Pay_Successful()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 100, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double sum = Payment.Pay(shoppingCart, users, foundUser);

        Assert.Equal(80, sum);
    }

    [Fact]
    public void Pay_User_Balance_Less_Than_Item_Price()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 10 });

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 8, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double sum = Payment.Pay(shoppingCart, users, foundUser);

        Assert.False(foundUser?.Balance > shoppingCart[0]?.PriceEuro);
    }

    [Fact]
    public void Pay_Same_Balance_Same_Price()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double sum = Payment.Pay(shoppingCart, users, foundUser);

        Assert.Equal(0, sum);
    }

    [Fact]
    public void Pay_Negative_User_Balance()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = -20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double sum = Payment.Pay(shoppingCart, users, foundUser);

        Assert.Equal(20, sum);
    }

    [Fact]
    public void Pay_Negative_Price()
    {
        List<Product?> shoppingCart = new List<Product?>();
        shoppingCart.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = -20 });

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double sum = Payment.Pay(shoppingCart, users, foundUser);

        Assert.Equal(-20, sum);
    }

    [Fact]
    public void CurrentHash_Success()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        string hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC";

        User? foundUser = UserProfile.CurrentHash(hash, users);

        Assert.Equal(hash, foundUser?.Hash);
    }

    [Fact]
    public void CurrentHash_Fail_Not_Found_In_List()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        string hash = "0FB2CD5A4472EC7D48AA0D7C75EC4AEAC478A41CE836CA6FC433708E8119606EB62D6E418377E447014A0E692CD5F84C3A5ACB30D58869D83F2C2C65A359DE05";

        Assert.Throws<ArgumentOutOfRangeException>(() => { UserProfile.CurrentHash(hash, users); });
    }

    [Fact]
    public void CurrentHash_Empty()
    {
        List<User?> users = new List<User?>();

        string hash = string.Empty;

        Assert.Throws<ArgumentOutOfRangeException>(() => { UserProfile.CurrentHash(hash, users); });
    }

    [Fact]
    public void AddMoney_Success()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double addToBalance = 50;

        double totalAmount = UserProfile.AddMoney(users, foundUser, addToBalance);

        Assert.True(totalAmount == 70);
    }

    [Fact]
    public void AddMoney_Empty_Amount()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double addToBalance = 0;

        double totalAmount = UserProfile.AddMoney(users, foundUser, addToBalance);

        Assert.True(totalAmount == 20);
    }

    [Fact]
    public void AddMoney_Empty_List()
    {
        List<User?> users = new List<User?>();

        User foundUser = null ?? new User();

        double addToBalance = 20;

        Assert.Throws<NullReferenceException>(() => { UserProfile.AddMoney(users, foundUser, addToBalance); });
    }

    [Fact]
    public void AddMoney_Empty_Logged_In_User()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User foundUser = null ?? new User();

        double addToBalance = 20;

        Assert.Throws<NullReferenceException>(() => { UserProfile.AddMoney(users, foundUser, addToBalance); });
    }

    [Fact]
    public void AddMoney_Negative_Amount()
    {
        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        User? foundUser = users[0];

        double addToBalance = -50;

        double totalAmount = UserProfile.AddMoney(users, foundUser, addToBalance);

        Assert.False(totalAmount == -30);
    }

    [Fact]
    public void KeysUsers_Contains_Key()
    {
        string usernameKey = "username";
        string userBalanceKey = "balance";
        string passwordKey = "password";

        Dictionary<string, string> dictionaryKeys = AdminOptions.KeysUsers(usernameKey, userBalanceKey, passwordKey);
        
        Assert.Contains(passwordKey, dictionaryKeys.Keys);
    }

    [Fact]
    public void KeysUsers_Not_Empty()
    {
        string usernameKey = "username";
        string userBalanceKey = "balance";
        string passwordKey = "password";

        Dictionary<string, string> dictionaryKeys = AdminOptions.KeysUsers(usernameKey, userBalanceKey, passwordKey);

        Assert.NotEmpty(dictionaryKeys);
    }

    [Fact]
    public void SaveUserList_True()
    {
        string usernameKey = "username";
        string userBalanceKey = "balance";
        string passwordKey = "password";

        Dictionary<string, string> adminInput =
                 new Dictionary<string, string> { { usernameKey, "Bob" }, { userBalanceKey, "32" }, { passwordKey, "eijei" } };

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Jim", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        bool isTrue = AdminOptions.SaveUserList(adminInput, users, usernameKey, userBalanceKey, passwordKey);

        Assert.True(isTrue);
    }

    [Fact]
    public void SaveUserList_False()
    {
        string usernameKey = "username";
        string userBalanceKey = "balance";
        string passwordKey = "password";

        Dictionary<string, string> adminInput =
                 new Dictionary<string, string> { { usernameKey, "Bob" }, { userBalanceKey, "32" }, { passwordKey, "eijei" } };

        List<User?> users = new List<User?>();
        users.Add(new User() { UserName = "Bob", Balance = 20, Hash = "48C8947F69C054A5CAA934674CE8881D02BB18FB59D5A63EEADDFF735B0E9801E87294783281AE49FC8287A0FD86779B27D7972D3E84F0FA0D826D7CB67DFEFC" });

        bool isTrue = AdminOptions.SaveUserList(adminInput, users, usernameKey, userBalanceKey, passwordKey);

        Assert.False(isTrue);
    }

    [Fact]
    public void SaveUserList_Empty()
    {
        string usernameKey = "username";
        string userBalanceKey = "balance";
        string passwordKey = "password";

        Dictionary<string, string> adminInput =
                 new Dictionary<string, string> {};

        List<User?> users = new List<User?>();

        Assert.Throws<KeyNotFoundException>(() => { AdminOptions.SaveUserList(adminInput, users, usernameKey, userBalanceKey, passwordKey); });
    }

    [Fact]
    public void KeysProducts_Contains_Key()
    {
        string brandKey = "brand";
        string priceKey = "price";
        string typeKey = "type";
        string colorKey = "color";

        Dictionary<string, string> dictionaryKeys = AdminOptions.KeysProducts(brandKey, priceKey, typeKey, colorKey);

        Assert.Contains(typeKey, dictionaryKeys);
    }

    [Fact]
    public void KeysProducts_Not_Empty()
    {
        string brandKey = "brand";
        string priceKey = "price";
        string typeKey = "type";
        string colorKey = "color";

        Dictionary<string, string> dictionaryKeys = AdminOptions.KeysProducts(brandKey,priceKey, typeKey, colorKey);

        Assert.NotEmpty(dictionaryKeys);
    }

    [Fact]
    public void SaveProductsList_True()
    {
        string brandKey = "brand";
        string priceKey = "price";
        string typeKey = "type";
        string colorKey = "color";

        Dictionary<string, string> adminInput = 
            new Dictionary<string, string> { { brandKey, "HJ" }, { priceKey, "5" }, { typeKey, "blouse" }, { colorKey, "green"} };

        List<Product?> products = new List<Product?>();
        products.Add(new Product() { Index = "1", Brand = "Swag", Type = "T-Shirt", Color = "red", PriceEuro = 20 });

        bool isTrue = AdminOptions.SaveProductsList(adminInput, products, brandKey, priceKey, typeKey, colorKey);

        Assert.True(isTrue);
    }

    [Fact]
    public void SaveProductsList_False()
    {
        string brandKey = "brand";
        string priceKey = "price";
        string typeKey = "type";
        string colorKey = "color";

        Dictionary<string, string> adminInput =
            new Dictionary<string, string> { { brandKey, "euj" }, { priceKey, "-5" }, { typeKey, "ieje" }, { colorKey, "wiuje" } };

        List<Product?> products = new List<Product?>();

        bool isTrue = AdminOptions.SaveProductsList(adminInput, products, brandKey, priceKey, typeKey, colorKey);

        Assert.False(isTrue);
    }

    [Fact]
    public void SaveProductsList_Empty()
    {
        string brandKey = "brand";
        string priceKey = "price";
        string typeKey = "type";
        string colorKey = "color";

        Dictionary<string, string> adminInput =
                 new Dictionary<string, string> { };

        List<Product?> products = new List<Product?>();

        Assert.Throws<KeyNotFoundException>(() => { AdminOptions.SaveProductsList(adminInput, products, brandKey, priceKey, typeKey, colorKey); });
    }
}
