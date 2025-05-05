using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpsPolicy;
using Utf8Json;



namespace API_Backend
{
    public class product
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
        public static product[] products;
        static void Main(string[] args)
        {
            string Products_File_Path = "Products.json";
            byte[] Product_Data = File.ReadAllBytes(Products_File_Path);
            
            products = JsonSerializer.Deserialize<product[]>(Product_Data);

            IHostBuilder builder = new HostBuilder();
            
            
        }
        [RequireHttps]
        [ApiController]
        [Route("api/pagedata")]
        public class Fetch_Pagedata
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
            public IActionResult Product_Data()
            {
                IActionResult result;



                return result;
            }
        }
    }
}