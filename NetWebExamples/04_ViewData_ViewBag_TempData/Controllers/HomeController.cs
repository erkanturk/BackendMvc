using _04_ViewData_ViewBag_TempData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace _04_ViewData_ViewBag_TempData.Controllers
{
    public class HomeController : Controller
    {
      /*Controllerdan View'e veri taþýmak için kullanýlan yöntemler
       * ViewBag:Dinamik bir özellik olup controllerdan view e veri taþýr
       * ViewData: Sözlük(Dictonary) benzeri bir yapýdýr. controllerdan view e veri taþýr.
       * TempData:Geçici veri taþýmak için kullanýlýr ve iki sonuç (action,view) arasýnda veriyi taþýr.
       
       */
        public IActionResult Index()
        {
            ViewBag.ad = "Erkan";
            ArrayList liste=new ArrayList();
            liste.Add("A");
            liste.Add(10);
            ViewBag.liste = liste;
            ViewBag.sonuc = true;
            //viewdata key-value iliþkisiyle verileri tutar ve 1 action boyunca geçerlidir.
            ViewData["soyad"] = "Türk";
            TempData["Cinsiyet"] = "Erkek";
            TempData.Keep("Cinsiyet");//2 den fazla actionda iþlem yapacaksa buradaki sonucu keep ile tutuyoruz.
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.text = ViewBag.ad;
            TempData["c"] = TempData["Cinsiyet"];
            return View();
        }

        
    }
}
