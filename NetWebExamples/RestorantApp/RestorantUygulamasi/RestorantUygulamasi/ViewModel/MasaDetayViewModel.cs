using RestorantUygulamasi.Models;

namespace RestorantUygulamasi.ViewModel
{
    public class MasaDetayViewModel
    {
        public Masa Masa { get; set; }
        public List<Rezervasyon> BugunRezervasyonlar { get; set; }
        public int? AktifRezervasyonId { get; set; }

    }
}
