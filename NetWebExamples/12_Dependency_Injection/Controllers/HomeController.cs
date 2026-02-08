using _12_Dependency_Injection.Models;
using _12_Dependency_Injection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _12_Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {

        private readonly TransientRandomNumberService _transientService;
        private readonly TransientRandomNumberService _transientService1;
        private readonly ScopedRandomNumberService _scopedService;
        private readonly ScopedRandomNumberService _scopedService1;
        private readonly SingletonRandomNumberService _singletonService;
        private readonly SingletonRandomNumberService _singletonService1;


        public HomeController(
            TransientRandomNumberService transientService,
            TransientRandomNumberService transientService1, 
            ScopedRandomNumberService scopedService,
            ScopedRandomNumberService scopedService1, 
            SingletonRandomNumberService singletonService,
            SingletonRandomNumberService singletonService1)
        {
            _transientService = transientService;
            _transientService1 = transientService1;
            _scopedService = scopedService;
            _scopedService1 = scopedService1;
            _singletonService = singletonService;
            _singletonService1 = singletonService1;
        }

        public IActionResult Index()
        {
            var transientValue1 = _transientService.GetRandomNumber();
            var transientValue2 = _transientService1.GetRandomNumber();
            var scopedValue1 = _scopedService.GetRandomNumber();
            var scopedValue2 = _scopedService1.GetRandomNumber();
            var singletonValue1 = _singletonService.GetRandomNumber();
            var singletonValue2 = _singletonService1.GetRandomNumber();

            ViewBag.TransientValue1 = transientValue1;
            ViewBag.TransientValue2 = transientValue2;
            ViewBag.ScopedValue1 = scopedValue1;
            ViewBag.ScopedValue2 = scopedValue2;
            ViewBag.SingletonValue1 = singletonValue1;
            ViewBag.SingletonValue2 = singletonValue2;
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}
