using Microsoft.EntityFrameworkCore;

namespace Entity_FrameWork_Core.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Kurs> Kurslar { get; set; }
    public DbSet<Ogrenci> Ogrenciler { get; set; }
    public DbSet<KursKayit> KursKayitlari { get; set; }
    public DbSet<Ogretmen> Ogretmenler { get; set; }

}


// code-first => entity , dbcontext => database (sqlite)
// database-first => sql server
