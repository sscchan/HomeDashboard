using Microsoft.AspNetCore.Mvc;

namespace HomeDashboard.WebApplication.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("./swagger");
        }
    }
}