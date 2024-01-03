using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController1 : ControllerBase
    {
        public IActionResult GetProduct()
        {
            return Ok(new {Id=1,Name="Kalem",Price=20});
        }
    }
}
