using RestorantUygulamasi.Models;

namespace RestorantUygulamasi.DataContext
{
    public static class DbInitializer
    {
        public static void Initialize(RestorantContext context)
        {
            context.Database.EnsureCreated();//eğer database oluşturulmamışsa database'i oluştur.
            if (!context.Masalar.Any())
            {
                for (int i = 1; i <= 20; i++)
                {
                    context.Masalar.Add(new Masa { MasaNumarasi = i, DoluMu = false });
                }
                context.SaveChanges();
            }
            if (!context.Kullanicilar.Any())
            {
                context.Kullanicilar.Add(new Kullanici()
                {
                    KullaniciAdi = "Admin",
                    Sifre = "1234",
                    Rol = "Admin"
                });
                context.SaveChanges();
            }

        }
    }
}
