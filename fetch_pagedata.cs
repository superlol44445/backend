using API_Backend;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Text;

namespace Backend
{
    //[RequireHttps]
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
        public IActionResult Product_Data([FromQuery] int id)
        {

            foreach (var product in productData.products)
            {
                if (product.id == id)
                {
                    var stream = new MemoryStream();
                    var writer = new Utf8JsonWriter(stream);

                    writer.WriteStartObject();
                    writer.WriteString("name", product.name);
                    writer.WriteString("description", product.description);
                    writer.WriteNumber("id", product.id);
                    writer.WriteNumber("cost", product.cost);
                    writer.WriteEndObject();

                    writer.Flush(); // Ensure data is written

                    string json = Encoding.UTF8.GetString(stream.ToArray());
                    return Ok(json);
                }
            }
            return StatusCode(403);
        }
    }
}
