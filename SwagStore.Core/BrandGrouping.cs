using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using swagStore.Business;
using swagStore.Business.Properties;

namespace SwagStore.Core
{
    public class BrandGrouping
    {
        public static List<Brands> BrandList()
        {
            List<Product> products = JsonFiles.ProductDeserializer();
            List<string> brands = products.Select(x => x.Brand).Distinct().ToList();
            List<string> brandIds = new List<string> { "001", "002", "003", "004", "005", "006", "007", "008", "009", "010", "011", "012", "013", "014" };

            List<Brands> brandList = brandIds.Zip(brands, (id, brand) => new Brands
            {
                Id = id,
                Brand = brand,
            }).ToList();

            return brandList;
        }

        public static void UpdateProducts()
        {
            List<Brands> brandsList = BrandList();
            List<Product> products = JsonFiles.ProductDeserializer();
            foreach (var item in products)
            {
                var match = brandsList.FirstOrDefault(x => x.Brand == item.Brand);
                if (match != null)
                {
                    item.Brand = match.Id;
                }
            }

            JsonFiles.ProductSerializer(products);
        }
    }
}
