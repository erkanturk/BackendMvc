using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace _01_ProgramRoute.Controllers
{
    public class HomeController : Controller
    {
        //action 
        public IActionResult Index() //Method=>Geriye IActionResult (iþ sonucu döndüren method)
                                    // döndüren bir methoddur IActionResult geriye bir sonuç sayfasý döndürür.
        {
            return View();//method geriye IActionResult döndürmek zorunda olduðundan bize bir view döndürecek 
                          //Home/Index
        }

        public IActionResult About()//sað týk add view
        {
            return View();
        }

     
    }
}
