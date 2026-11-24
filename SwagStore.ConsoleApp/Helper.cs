using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swagStore.ConsoleApp
{
    /// <summary>
    /// For methods that dont fit anywhere else
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// User input as method
        /// </summary>
        /// <returns></returns>
        public static string UserInput() 
        {
            string userInput = Console.ReadLine() ?? "";
            return userInput;
        }
    }
}
