using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using swagStore.Business;
using swagStore.Business.Properties;
using Microsoft.AspNetCore.Mvc;

namespace SwagStore.Api.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : ControllerBase
    {
        [HttpPost("changePrice")]
        public IActionResult PriceChange([FromBody] ProductRequest request)
        {
            if (request == null) { return BadRequest("Empty index and price."); }

            string index = request.Index;
            double price = request.PriceEuro;

            List<Product> products = JsonFiles.ProductDeserializer();
            var targetProduct = products.FirstOrDefault(x => x.Index == index);
            if (targetProduct == null) { return BadRequest("Product doesn't exist."); }

            targetProduct.PriceEuro = price;

            JsonFiles.ProductSerializer(products);

            return Ok(targetProduct);
        }

        [HttpPost("changeColor")]
        public IActionResult ColorChange([FromBody] ColorChange request)
        {
            if (request == null) { return BadRequest("Empty index and color."); }
            string index = request.Index;
            string color = request.Color;

            List<Product> products = JsonFiles.ProductDeserializer();
            var targetProduct = products.FirstOrDefault(x => x.Index == index);
            if (targetProduct == null) { return BadRequest("Product doesn't exist."); }
            targetProduct.Color = color;

            JsonFiles.ProductSerializer(products);

            return Ok(targetProduct);
        }

        [HttpPost("changeBrand")]
        public IActionResult BrandChange([FromBody] ChangeRequest request)
        {
            if (request == null) { return BadRequest("Empty index and brand."); }
            string index = request.Index;
            string brand = request.Brand;

            List<Product> products = JsonFiles.ProductDeserializer();
            int intIndex = 0;
            Int32.TryParse(index, out intIndex);
            if (intIndex > products.Count) { return BadRequest("Index doesn't exist."); }

            List<Brands> brandsList = JsonFiles.BrandsDeserializer();
            var targetBrand = brandsList.FirstOrDefault(x => x.Brand == brand);

            if (targetBrand == null && intIndex > 0)
            {
                int newId = brandsList.Count + 1;
                string stringId = "0" + newId.ToString();
                brandsList.Add(new Brands { Id = stringId, Brand = brand });

                JsonFiles.BrandsSerializer(brandsList);


                List<Product> productsList = JsonFiles.ProductDeserializer();
                var targetProd = productsList.FirstOrDefault(x => x.Index == index);
                if (targetProd == null) { return BadRequest("Product doesn't exist."); }
                targetProd.Brand = stringId;

                JsonFiles.ProductSerializer(productsList);

                return Ok(targetProd);
            }
            else
            {
                if (targetBrand == null) { return BadRequest("Empty id."); }
                string id = targetBrand.Id;
                products = JsonFiles.ProductDeserializer();
                var targetProduct = products.FirstOrDefault(x => x.Index == index);
                if (targetProduct == null) { return BadRequest("Product doesn't exist."); }
                targetProduct.Brand = id;

                JsonFiles.ProductSerializer(products);

                return Ok(targetProduct);
            }
        }

    }

    public class ProductRequest
    {
        public string Index { get; set; }
        public double PriceEuro { get; set; }
    }

    public class ColorChange
    {
        public string Index { get; set; }
        public string Color { get; set; }
    }

    public class ChangeRequest
    {
        public string Index { get; set; }
        public string Brand { get; set; }
    }
}
