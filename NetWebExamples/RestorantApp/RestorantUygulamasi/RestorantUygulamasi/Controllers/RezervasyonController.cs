using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestorantUygulamasi.DataContext;
using RestorantUygulamasi.Models;

namespace RestorantUygulamasi.Controllers
{
    public class RezervasyonController: Controller
    {
        private readonly RestorantContext _context;
        public RezervasyonController(RestorantContext context)
        {
            _context = context;
        }
        public IActionResult Detay(int masaId)
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
            var rezervasyonlar = _context.Rezervasyons.Where(r => r.MasaId == masaId && r.RezervasyonTarihi >= DateTime.Today).ToList();
            ViewBag.MasaNumarasi = masa.MasaNumarasi;
            ViewBag.MasaId = masa.Id;
            return View(rezervasyonlar);
        }
        public IActionResult Ekle(int masaId)
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
            ViewBag.MasaId = masaId;
            ViewBag.MasaNumarasi = masa.MasaNumarasi;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(int masaId, string musteriAdi, DateTime rezervasyonTarihi)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var masa = await _context.Masalar.FindAsync(masaId);
            if (masa == null)
            {
                return NotFound("Masa Bulunamadı");
            }
            var mevcutRezervasyon = await _context.Rezervasyons.FirstOrDefaultAsync
                (r => r.MasaId == masaId && r.RezervasyonTarihi.Date == rezervasyonTarihi.Date);
            if (mevcutRezervasyon != null)
            {
                ViewBag.ErrorMessage = "Bu masada seçilen tarihte müsaitlik bulunmamaktadır";
                ViewBag.MasaId = masaId;
                ViewBag.MasaNumarasi = masa.MasaNumarasi;
                return View();
            }
            var rezervasyon = new Rezervasyon()
            {
                MasaId = masaId,
                MusteriAdi = musteriAdi,
                RezervasyonTarihi = rezervasyonTarihi
            };
            LogKaydet(masaId, musteriAdi, rezervasyonTarihi);
            await _context.Rezervasyons.AddAsync(rezervasyon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detay", new { masaId = masaId });
        }
        private void LogKaydet(int masaId, string musteriAdi, DateTime rezervasyonTarihi)
        {
            var log = new LogTablosu()
            {
                MasaId = masaId,
                MusteriAdi = musteriAdi,
                RezervasyonTarihi = rezervasyonTarihi
            };
            _context.LogTablosu.Add(log);
            _context.SaveChanges();
        }
        public IActionResult Duzenle(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyon = _context.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return NotFound("Masa Bulunamadı");
            }
            var masa = _context.Masalar.Find(rezervasyon.MasaId);
            ViewBag.MasaNumarasi = masa.MasaNumarasi;
            ViewBag.MasaId = masa.Id;
            return View(rezervasyon);
        }
        [HttpPost]
        public IActionResult Duzenle(int id, string musteriAdi, DateTime rezervasyonTarihi)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyon = _context.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return NotFound("Masa Bulunamadı");
            }
            var masa = _context.Masalar.Find(rezervasyon.MasaId);
            var mevcutRezervasyon = _context.Rezervasyons
                .FirstOrDefault(r => r.MasaId == rezervasyon.MasaId && r.RezervasyonTarihi.Date == rezervasyonTarihi.Date);
            if (mevcutRezervasyon != null)
            {
                if (rezervasyon.MasaId == masa.Id && rezervasyon.RezervasyonTarihi.Date == rezervasyonTarihi.Date)
                {
                    rezervasyon.MusteriAdi = musteriAdi;
                    rezervasyon.RezervasyonTarihi = rezervasyonTarihi;
                    _context.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu masada seçilen tarihte başka bir rezervasyon var.";
                    ViewBag.MasaNumarasi = masa.MasaNumarasi;
                    ViewBag.MasaId = masa.Id;
                    return View(rezervasyon);
                }
            }
            if (mevcutRezervasyon == null)
            {
                rezervasyon.MusteriAdi = musteriAdi;
                rezervasyon.RezervasyonTarihi = rezervasyonTarihi;
                _context.SaveChanges();
            }
            return RedirectToAction("Detay", new { masaId = rezervasyon.MasaId });
        }
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyonlar = _context.LogTablosu.ToList();
            return View(rezervasyonlar);
        }
        public IActionResult Sil(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyon = _context.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return NotFound("Masa Bulunamadı");
            }
            var masa = _context.Masalar.Find(rezervasyon.MasaId);
            ViewBag.MasaNumarasi = masa.Id;
            ViewBag.MasaId = masa.Id;
            ViewBag.MustediAdi = rezervasyon.MusteriAdi;
            return View(rezervasyon);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilOnay(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
            {
                return RedirectToAction("Login", "Account");
            }
            var rezervasyon = _context.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return NotFound("Masa Bulunamadı");
            }

           
            _context.Rezervasyons.Remove(rezervasyon);

            _context.SaveChanges();
            MasaBosalt(rezervasyon.MasaId);
            return RedirectToAction("Detay", new { masaId = rezervasyon.MasaId });
        }
        private void MasaBosalt(int masaId)
        {
            var masa = _context.Masalar.Find(masaId);
            if (masaId != null)
            {
                masa.DoluMu = !masa.DoluMu;

                

                _context.SaveChanges();
            }
        }


    }
}
