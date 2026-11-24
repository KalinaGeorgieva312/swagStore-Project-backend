using swagStore.Business;
using swagStore.Business.Properties;
using swagStore.ConsoleApp;
using SwagStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagStore.ConsoleApp
{
    /// <summary>
    /// Contains messages related to the profile menu, password change & adding money to acoount
    /// </summary>
    public class DisplayUserProfile
    {
        /// <summary>
        /// Displays instructions and messages for password change & calls methods that do the password change & save it in the json file
        /// </summary>
        /// <param name="users">List of users</param>
        public static void Password(List<User?> users)
        {
            Console.WriteLine("\nAltes Passwort:");
            string currentPassword = Helper.UserInput();

            string hash = Authentication.HashPassword(currentPassword);

            User? currentHash = UserProfile.CurrentHash(hash, users);

            if (currentHash == null)
            {
                Console.WriteLine("\nPasswort kann nicht leer sein.");
            }
            else
            {
                Console.WriteLine("\nNeues Passwort:");
                string newPassword = Helper.UserInput().Trim();

                if (newPassword != string.Empty)
                {
                    string newHash = Authentication.HashPassword(newPassword);

                    if (hash != newHash)
                    {
                        UserProfile.ChangePassword(hash, newHash, users);
                        Console.WriteLine("\nDas Passwort wurde erfolgreich geändert.");
                    }

                    else
                    {
                        Console.WriteLine("\nSie können nicht dasselbe Password verwenden!");
                    }
                }

                else
                {
                    Console.WriteLine("Password kann nicht leer sein!");
                }

            }
        }

        /// <summary>
        /// Displays instructions and messages for adding money to user account & calls method that does the math and json file save
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="foundUser">Currently logged in user</param>
        public static void AccountMoney(List<User?> users, User? foundUser)
        {
            Console.WriteLine("\nIhr Kontostand: " + foundUser?.Balance + " Euro");
            Console.WriteLine("\nGeldbetrag hinzufügen:");

            double addToBalance = double.Parse(Helper.UserInput());

            if (addToBalance > 0)
            {
                UserProfile.AddMoney(users, foundUser, addToBalance);
                Console.WriteLine("\nDas Geld wurde erfolgreich auf das Konto hinzugefügt.");
                Console.WriteLine("Ihr aktueller Kontostand: " + foundUser?.Balance);
            }

            else
            {
                Console.WriteLine("\nDas Geldbetrag kann nicht 0 oder negativ sein!");
            }
        }

        /// <summary>
        /// Profile menu instructions display & controls choice between password change & adding money to account
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="foundUser">Currently logged in user</param>
        public static void ProfileDisplay(List<User?> users, User? foundUser)
        {
            Console.WriteLine("\nWillkommen im Profilmenü.");
            Console.WriteLine("Wählen Sie 1, um Ihr Password zu ändern, oder wählen Sie 2, um Geld auf Ihrer Konto einzuzahlen");
            string userChoice = Helper.UserInput();

            switch (userChoice)
            {
                case "1":
                    try
                    {
                        Password(users);
                    }

                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("\nPassword kann nicht leer sein!");
                    }
                    break;
                case "2":
                    try
                    {
                        AccountMoney(users, foundUser);
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("\nDas Geldbetrag kann nicht Buchstaben haben oder leer sein!");
                    }
                    break;
                default:
                    Console.WriteLine("\nFalsche Eingabe!");
                    break;
            }
        }
    }
}
