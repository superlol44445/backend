using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utf8Json;



namespace API_Backend
{
    public class Product
    {
        public string name { get; set; }
        public string? description { get; set; }
        public int id { get; set; }
        public int cost { get; set; }
        public int amount_bought { get; set; }
        public int[]? coupon_ids { get; set; }    
        public string[]? categories { get; set; }
    }
    internal class Program
    {
        public static Product[] products;
        static void Main(string[] args)
        {
            string Products_File_Path = "Products.json";
            byte[] Product_Data = File.ReadAllBytes(Products_File_Path);
            
            products = JsonSerializer.Deserialize<Product[]>(Product_Data);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();

            Thread thread = new Thread(app.Run); 
            thread.Start();
            while (true)
            {
                Console.WriteLine("11");
                Thread.Sleep(100);
            }
            
        }
        [RequireHttps]
        [ApiController]
        [Route("api/pagedata")]
        public class Fetch_Pagedata : ControllerBase
        {
            [HttpGet()]
            [Route("Index")]
            
            public IActionResult Index_Data()
            {
                IActionResult result;
                


                return result;
            }
            [HttpGet()]
            [Route("products_page")]
            public IActionResult Products_Page_Data()
            {
                IActionResult result;



                return result;
            }
            [HttpGet()]
            [Route("product")]
            public IActionResult Product_Data([FromQuery] int id)
            {
                IActionResult result;



                return result;
            }
        }
    }
}