using _13_StateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _13_StateManagement.Controllers
{
    public class HomeController : Controller
    {
        /* Session(Oturum)-Cookie (Çerezler)
         * Session statler uygulama çalýþtýðý süre boyunca 
         * (Oturum boyunca) verileri saklamamýzý saðlayan yapýlardýr.
         * Oturum sona erdiðinde (Uygulama kapatýldýðýnda yada sonlandýrýldýðýnda)
         * sessiondaki veriler silinir. Sessionda özel bilgilerin saklanmasý önerilmez
         * Sessionlar sunucu tarafýnda saklanýr. Sessionlara eriþmek için HttpContext.Session kullanýlýr.
         */

        public IActionResult Index()
        {
            HttpContext.Session.SetString("UserName", "Erkan Türk");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            /*Cookie (Çerezler) - Cookie, kullanýcýlarýn tarayýcýlarýnda saklanan küçük veri parçalarýdýr.
             * Key value iliþkisiyle tutulur Bir expire date (son kullaným süresi) vardýr
             * Bu süre dolduðunda cookie otomatik olarak silinir. Cookieler client tarafýnda saklanýr. Cookielere eriþmek için
             * HttpContext.Response.Cookies ve HttpContext.Request.Cookies kullanýlýr.
             
             */
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30), // Cookie'nin 30 dakika sonra sona ermesini saðlar
                HttpOnly = true, // Cookie'nin JavaScript tarafýndan eriþilmesini engeller
                IsEssential = true // GDPR uyumluluðu için gerekli olabilir
            };
            Response.Cookies.Append("UserName", "Erkan Türk", cookieOptions);
            var cookieUserName = Request.Cookies["UserName"];
            ViewBag.CookieUserName = cookieUserName;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
