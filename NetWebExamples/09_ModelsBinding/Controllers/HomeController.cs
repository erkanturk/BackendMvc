using _09_ModelsBinding.Models;
using _09_ModelsBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _09_ModelsBinding.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult HomePage()
        {
            Kisi kisi = new Kisi()//instance örneklem nesne oluþturma
            {
                Ad = "Erkan",
                Soyad = "Türk",
                Yas = 31
            };
            return View(kisi);
        }
        public IActionResult HomePage2()
        {
            Kisi kisi = new Kisi()//instance örneklem nesne oluþturma
            {
                Ad = "Erkan",
                Soyad = "Türk",
                Yas = 31
            };
            Adres adres = new Adres()
            {
                AdresTanim = "Kadýköy/Caferaða mah.",
                Sehir = "istanbul"
            };
            homePageViewModel model = new homePageViewModel()
            {
                KisiNesnesi = kisi,
                AdresNesnesi=adres,
            };
            return View(model);
        }


    }
}
