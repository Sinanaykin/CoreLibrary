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
            return Ok(new {Id=1,Name="Kalem",Price=10});
        }
        [HttpPost]
        public IActionResult SaveProduct()
        {
            return Ok(new { status="Success"});
        }
    }
}
