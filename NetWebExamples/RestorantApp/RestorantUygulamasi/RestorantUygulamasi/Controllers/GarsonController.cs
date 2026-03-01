using Microsoft.AspNetCore.Mvc;
using RestorantUygulamasi.DataContext;
using RestorantUygulamasi.Models;
using RestorantUygulamasi.ViewModel;

namespace RestorantUygulamasi.Controllers
{
    public class GarsonController : Controller
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
        [HttpPost]
        public IActionResult DurumDegistir(int masaId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var masa = _context.Masalar.Find(masaId);
            if (masa == null)
            {
                return NotFound("Masa Bulunamadı");
            }
            masa.DoluMu = !masa.DoluMu;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RezervasyonAktifEt(int rezervasyonId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyon = _context.Rezervasyons.Find(rezervasyonId);
            if (rezervasyon == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }
            var masa = _context.Masalar.Find(rezervasyon.MasaId);
            if (masa is null)
            {
                return NotFound("Masa bulunamadı");
            }
            masa.DoluMu = true;
            HttpContext.Session.SetInt32($"AktifRezervasyonId_ {masa.Id}", rezervasyonId);
            _context.SaveChanges();
            return RedirectToAction("MasaDetay", new { masaId = masa.Id });
        }
        public IActionResult MasaDetay(int masaId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var masa = _context.Masalar.Find(masaId);
            if (masa == null)
            {
                return RedirectToAction("Masa Bulunamadı");
            }
            var bugunRezervasyonlar =
                _context.Rezervasyons.Where(r => r.MasaId == masaId && r.RezervasyonTarihi.Date == DateTime.Today).ToList();
            int? aktifRezervasyonId = HttpContext.Session.GetInt32($"AktifRezervasyonId_{masaId}");
            var viewModel = new MasaDetayViewModel()
            {
                Masa = masa,
                BugunRezervasyonlar = bugunRezervasyonlar,
                AktifRezervasyonId = aktifRezervasyonId
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult MasayiBosalt(int masaId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var masa = _context.Masalar.Find(masaId);
            if (masa == null)
            {
                return NotFound("Masa bulunamadı");
            }
            int? aktifRezervasyonId = HttpContext.Session.GetInt32($"AktifRezervasyonId_{masaId}");
            if (aktifRezervasyonId.HasValue)
            {
                var aktifRezervasyon = _context.Rezervasyons.Find(aktifRezervasyonId.Value);
                if (aktifRezervasyonId != null)
                {
                    _context.Rezervasyons.Remove(aktifRezervasyon);
                }
            }
            masa.DoluMu = false;
            HttpContext.Session.Remove($"AktifRezervasyonId_{masaId}");
            _context.SaveChanges();
            return RedirectToAction("MasaDetay", new { masaId = masaId });

        }
    }
}
