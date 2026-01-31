using Microsoft.AspNetCore.Mvc;

namespace _01_ProgramRoute.Controllers
{
    public class ContactController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
