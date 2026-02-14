using Microsoft.AspNetCore.Mvc;

namespace _16_Web_Api.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
