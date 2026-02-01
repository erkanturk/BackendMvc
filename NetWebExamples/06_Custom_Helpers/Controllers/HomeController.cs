using _06_Custom_Helpers.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _06_Custom_Helpers.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            ViewBag.Message = StringHelper.CapitalizeWord("databaseten gelen veri");
            return View();
        }



    }
}
