using Microsoft.AspNetCore.Mvc;
using swagStore.Business;
using swagStore.Business.Properties;
using SwagStore.Api.Services;
using SwagStore.Core;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace SwagStore.Api.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ShoppingCartsManager _manager;

        // Optional: IWebHostEnvironment injizieren, um relative Pfade zum Projekt/Root zu bestimmen
        public ProductsController(IWebHostEnvironment env, ShoppingCartsManager manager)
        {
            _env = env;
            _manager = manager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            string fileName = Path.Combine(_env.ContentRootPath, "ProductsList.json");

            if (!System.IO.File.Exists(fileName))
            {
                return NotFound($"Die Datei unter '{fileName}' wurde nicht gefunden.");
            }

            try
            {

                List<Product?>? products = JsonFiles.ProductDeserializer();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }

        /*[HttpGet("GetByBrand")]
        public async Task<IActionResult> GetProductsByBrand()
        {
            string fileName2 = Path.Combine(_env.ContentRootPath, "ProductsGroupBrand.json");
            if (!System.IO.File.Exists(fileName2))
            {
                return NotFound($"Die Datei unter '{fileName2}' wurde nicht gefunden.");
            }
            try
            {
                List<Product> byBrand = JsonFiles.ByBrandDeserializer();
                return Ok(byBrand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }*/

        [HttpGet("GetByBrand")]
        public ActionResult<IEnumerable<string>> GetBrands()
        {
            List<Brands> brandList = JsonFiles.BrandsDeserializer();
            
            /*List<Product> products = JsonFiles.ProductDeserializer();
            List<string> brands = products.Select(x => x.Brand!).Distinct().ToList();*/
            return Ok(brandList);
        }

        [HttpGet("GetByType")]
        public ActionResult<IEnumerable<string>> GetTypes()
        {
            List<Product> products = JsonFiles.ProductDeserializer();
            List<string> types = products.Select(x => x.Type!).Distinct().ToList();
            return Ok(types);
        }


        /*[HttpGet("GetByType")]
        public async Task<IActionResult> GetProductsByType()
        {
            string filename1 = Path.Combine(_env.ContentRootPath, "ProductsGroupType.json");
            if (!System.IO.File.Exists(filename1))
            {
                return NotFound($"Die Datei unter '{filename1}' wurde nicht gefunden.");
            }
            try
            {
                List<Product> byType = JsonFiles.ByTypeDeserialization();
                return Ok(byType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }*/

        [HttpPost("PostProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            List<Product> products = JsonFiles.ProductDeserializer();

            products.Add(product);
            product.Index = (products.IndexOf(product) + 1).ToString();

            JsonFiles.ProductSerializer(products);

            return Ok(product);
        }

        [HttpPut("{index}")]
        public async Task<IActionResult> ChangeProduct(string index, Product changedProduct)
        {
            List<Product> products = JsonFiles.ProductDeserializer();

            Product? product = ShoppingCart.MatchUserInputAndList(index, products);
            if (product == null)
            {
                return NotFound();
            }

            product.Brand = changedProduct.Brand;
            product.PriceEuro = changedProduct.PriceEuro;
            product.Type = changedProduct.Type;
            product.Color = changedProduct.Color;

            JsonFiles.ProductSerializer(products);

            return Ok(changedProduct);
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteProduct(string index)
        {
            List<Product> products = JsonFiles.ProductDeserializer();

            Product? product = ShoppingCart.MatchUserInputAndList(index, products);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);

            JsonFiles.ProductSerializer(products);
            return NoContent();
        }


        [HttpPost("shoppingCart")]
        public IActionResult shoppingCart([FromBody] ShopRequest request)
        {
            //_manager.MyProperty = _manager.MyProperty + 1;

            if (request == null) { return BadRequest("Empty username & shopping cart."); }

            string names = request.Name;

            if (!_manager.userShoppingCarts.TryAdd(names, request.Items))
            {
                if (_manager.userShoppingCarts.TryGetValue(names, out var existingCart))
                {
                    existingCart.AddRange(request.Items);
                }
            }

            return Ok();
        }

        [HttpGet("{name}")]
        public IActionResult EachUserCart(ShoppingCartsManager dictionary, string name)
        {
            if (dictionary.userShoppingCarts.TryGetValue(name, out List<Product> value))
            {
                return Ok(value);
            }
            else
            {
                return NotFound("No shopping cart for this user.");
            }
        }

        [HttpGet("price{name}")]
        public IActionResult TotalPrice(ShoppingCartsManager dictionary, string name)
        {
            if (dictionary.userShoppingCarts.TryGetValue(name, out List<Product> value))
            {
                double totalSum = ShoppingCart.ShoppingCartSum(value);

                return Ok(totalSum);
            }

            else
            {
                return NotFound("Empty shopping cart.");
            }
        }

        [HttpGet("payment{name}")]
        public IActionResult Purchase(ShoppingCartsManager dictionary, string name)
        {
            List<User> users = JsonFiles.UserDeserializer();
            User currentUser = users.FirstOrDefault(x => x?.UserName == name);

            if (dictionary.userShoppingCarts.TryGetValue(name, out List<Product> value))
            {
                var userBalance = Payment.Pay(value, users, currentUser);
                value.Clear();
                return Ok(userBalance);
            }
            else
            {
                return NotFound("Missing or empty shopping cart.");
            }
        }
    }

    public class ShopRequest
    {
        public List<Product> Items { get; set; } = [];


        public string? Name { get; set; }
    }


}
