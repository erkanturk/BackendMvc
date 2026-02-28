using Microsoft.EntityFrameworkCore;
using RestorantUygulamasi.Models;

namespace RestorantUygulamasi.DataContext
{
    public class RestorantContext: DbContext
    {
        public RestorantContext(DbContextOptions<RestorantContext> options) : base(options)
        {

        }
        public DbSet<Masa> Masalar { get; set; }
        public DbSet<Rezervasyon> Rezervasyons { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<LogTablosu> LogTablosu { get; set; }
    }
}
