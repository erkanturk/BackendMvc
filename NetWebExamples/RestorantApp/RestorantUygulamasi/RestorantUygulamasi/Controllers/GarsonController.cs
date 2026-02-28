using Microsoft.AspNetCore.Mvc;
using RestorantUygulamasi.DataContext;
using RestorantUygulamasi.Models;
using RestorantUygulamasi.ViewModel;

namespace RestorantUygulamasi.Controllers
{
    public class GarsonController: Controller
    {
        private readonly RestorantContext _context;
        public GarsonController(RestorantContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var masalar = _context.Masalar.ToList();
            var masaDetaylari = masalar.Select(masa => new MasaListeViewModel()
            {
                Masa = masa,
                AktifRezervasyon = GetAktifRezervasyon(masa.Id)
            }).ToList();
            return View(masaDetaylari);

        }
        private Rezervasyon GetAktifRezervasyon(int masaId)//Yardımcı method;
        {
            var aktifRezervasyonId = HttpContext.Session.GetInt32($"AktifRezervasyonId_ {masaId}");
            if (aktifRezervasyonId.HasValue)
            {
                return _context.Rezervasyons.Find(aktifRezervasyonId.Value);
            }
            return null;
        }

    }
}
