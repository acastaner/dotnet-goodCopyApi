using Microsoft.AspNetCore.Mvc;

namespace GoodCopyApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("<html><body><h1>Welcome to GoodCopyApi!</h1><p><a href=\"./swagger/index.html\">Documentation</a></body></html>", "text/html");
        }
    }
}