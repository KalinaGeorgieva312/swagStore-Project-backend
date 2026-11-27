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

        [HttpPost("addNewProduct")]
        public IActionResult NewProductAddition([FromBody] NewProduct request)
        {
            if (request == null) { return BadRequest("Empty product properties."); }

            List<Product> products = JsonFiles.ProductDeserializer();

            string brand = request.Brand;
            double price = request.PriceEuro;
            string type = request.Type;
            string color = request.Color;
            string index = (products.Count + 1).ToString();

            List<Brands> brandsList = JsonFiles.BrandsDeserializer();
            var targetBrand = brandsList.FirstOrDefault(x => x.Brand == brand);

            if (targetBrand == null)
            {
                int newId = brandsList.Count + 1;
                string stringId = "0" + newId.ToString();
                brandsList.Add(new Brands { Id = stringId, Brand = brand });

                JsonFiles.BrandsSerializer(brandsList);
            }

            targetBrand = brandsList.FirstOrDefault(x => x.Brand == brand);

            if (price > 0 && brand != string.Empty && type != string.Empty && color != string.Empty)
            {
                Product newProduct = new Product()
                {
                    Index = index,
                    Brand = targetBrand.Id,
                    PriceEuro = price,
                    Type = type,
                    Color = color
                };
                products.Add(newProduct);
                JsonFiles.ProductSerializer(products);
                return Ok();
            }

            return BadRequest("One or more properties are invalid.");
        }

        [HttpPost("changeUsername")]
        public IActionResult ChangeUsername([FromBody] Username request)
        {
            if (request == null) { return BadRequest("Empty request."); }

            string old = request.OldUname;
            string newUsername = request.NewUname;

            List<User> users = JsonFiles.UserDeserializer();
            var targetUser = users.FirstOrDefault(x => x.UserName == old);

            if (targetUser == null)
            {
                return BadRequest("This username doesn't exist.");
            }
            else
            {
                var exists = users.FirstOrDefault(x => x.UserName == newUsername);

                if (exists == null)
                {
                    targetUser.UserName = newUsername;
                    JsonFiles.UserSerializer(users);

                    return Ok(targetUser);
                }
                else
                {
                    return BadRequest("Username must be unique!");
                }
            }
        }

        [HttpPost("changeBalance")]
        public IActionResult ChangeBalance([FromBody] Money request)
        {
            if (request == null) { return BadRequest("Empty request"); }

            string username = request.Username;
            double balance = request.Balance;

            List<User> users = JsonFiles.UserDeserializer();
            var targetUser = users.FirstOrDefault(x => x.UserName == username);

            if (targetUser == null)
            {
                return BadRequest("This username doesn't exist.");
            }
            else
            {
                targetUser.Balance += balance;
                JsonFiles.UserSerializer(users);

                return Ok(targetUser);
            }
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] NewPassword request)
        {
            if (request == null) { return BadRequest("Empty request."); }

            string username = request.Username;
            string password = request.Password;

            List<User> users = JsonFiles.UserDeserializer();
            var targetUser = users.FirstOrDefault(x => x.UserName == username);

            if (targetUser == null)
            {
                return BadRequest("This username doesn't exist.");
            }
            else
            {
                string hash = Authentication.HashPassword(password);
                if (targetUser.Hash == hash)
                {
                    return BadRequest("Can't use the same password!");
                }
                else
                {
                    targetUser.Hash = hash;
                    JsonFiles.UserSerializer(users);

                    return Ok(targetUser);
                }
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

    public class NewProduct
    {
        public string Brand { get; set; }
        public double PriceEuro { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
    }

    public class Username
    {
        public string OldUname { get; set; }
        public string NewUname { get; set; }
    }

    public class Money
    {
        public string Username { get; set; }
        public double Balance { get; set; }
    }

    public class NewPassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
