using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<string> Products = new() { "Monitor", "Keyboard", "Mouse" };

        [HttpGet]
        public IActionResult Get() => Ok(Products);
    }
}

