using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swagStore.Business.Properties
{
    /// <summary>
    /// Parameters for the product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// To show in console numbered
        /// </summary>
        public string Index { get; set; } = string.Empty;

        /// <summary>
        /// Brand name
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// The price in Euro
        /// </summary>
        public double PriceEuro { get; set; }

        /// <summary>
        /// What kind of clothing item, e.g T-shirt, pants, etc
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Available colors, e.g blue, red, etc
        /// </summary>
        public string? Color { get; set; }
    }
}
