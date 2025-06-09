using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace API_Backend
{

    internal class Program
    {
        public static JObject ProductData;
        static void Main(string[] args)
        {
            string Products_File_Path = "Products.json";
            try
            {
                string json = File.ReadAllText(Products_File_Path);
                JObject Product_Data = JObject.Parse(json);
                if (Product_Data == null)
                {
                    Console.WriteLine("Invalid JSON array in file.");
                    return;
                }
                ProductData = Product_Data;
            }
            catch (Exception ex) { }

            Thread thread = new Thread(() => webbapp(args));
            thread.Start();

            var mostBoughtItemsPerCategory = new List<(string CategoryName, List<JObject> TopItems)>();

            foreach (var rootCategory in ProductData.Properties())
            {
                var allItems = new List<JObject>();
                foreach (var subCategory in (JObject)rootCategory.Value)
                {
                    foreach (var itemProperty in (JObject)subCategory.Value)
                    {
                        if (itemProperty.Value is JObject itemObj)
                        {
                            allItems.Add(itemObj);
                        }
                    }
                }

                var top4 = allItems
                    .Where(item => item["amount_bought"] != null)
                    .OrderByDescending(item => (int)item["amount_bought"])
                    .Take(4)
                    .ToList();

                mostBoughtItemsPerCategory.Add((rootCategory.Name, top4));
            }

            foreach (var (CategoryName, TopItems) in mostBoughtItemsPerCategory)
            {
                Console.WriteLine($"Top 4 most bought items in category '{CategoryName}':");
                foreach (var item in TopItems)
                {
                    Console.WriteLine($"- {item["name"]} ({item["amount_bought"]} bought)");
                }
                Console.WriteLine();
            }
        }
        public static void webbapp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(5000);
                options.ListenAnyIP(443, listenOptions => listenOptions.UseHttps());
            });
            builder.Services.AddControllers();

            var app = builder.Build();

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
    [ApiController]
    [Route("api/pagedata")]
    public class Fetch_Pagedata : ControllerBase
    {
        [HttpGet()]
        [Route("Index")]

        public IActionResult Index_Data()
        {




            return Ok();
        }
        [HttpGet()]
        [Route("products_page")]
        public IActionResult Products_Page_Data()
        {




            return Ok();
        }
        [HttpGet()]
        [Route("product")]
        public async Task<IActionResult> Product_Data([FromQuery] int id, [FromQuery] string part ="cpu", [FromQuery] string brand = "amd")
        {
            JObject product = Program.ProductData[part][brand][id.ToString()].ToObject<JObject>();
            

            if (product == null)
            {
                return StatusCode(404); // Product not found
            }

            var multipartContent = new MultipartFormDataContent();
            HttpContent content;
            content = new StringContent(product.ToString());

            multipartContent.Add(content, "productData");

            

            return File(await multipartContent.ReadAsStreamAsync(), "multipart/form-data");
        }

    }
}