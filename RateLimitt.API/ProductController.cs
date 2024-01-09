using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimitt.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new {Id=1,Name="Kalem",Price=20});
        }
        [HttpPost]
        public IActionResult SaveProduct()
        {
            return Ok(new { status="Success"});
        }


        [HttpPut]
        public IActionResult UpdateProduct()
        {
            return Ok();
        }

        [HttpGet("{name}/{price}")]
        public IActionResult GetProduct(string name,int price)
        {
            return Ok(name);
        }

    }
}
