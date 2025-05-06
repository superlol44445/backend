using Microsoft.AspNetCore.Http.HttpResults;
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
    public static class productData
    {
        public static Product[] products;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string Products_File_Path = "Products.json";
            byte[] Product_Data = File.ReadAllBytes(Products_File_Path);
            productData.products = JsonSerializer.Deserialize<Product[]>(Product_Data);

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
}