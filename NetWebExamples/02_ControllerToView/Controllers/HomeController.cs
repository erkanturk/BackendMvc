
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_ControllerToView.Controllers
{
    public class HomeController : Controller
    {

     
        public IActionResult Index()
        {
            var products = new List<string> { "Ürün 1", "Ürün 2", "Ürün 3" };
            ViewData["Products"]= products;//backend yapýsýndan view yapýsýna veri yollama
            return View();

        }

        public IActionResult Details(int id)
        {
            var product = $"Ürün {id} detaylarý";
            ViewData["Product"] = product;
            return View();
        }

    }
}
