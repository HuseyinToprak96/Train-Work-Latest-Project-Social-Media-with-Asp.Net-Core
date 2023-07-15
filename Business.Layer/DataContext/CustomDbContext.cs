using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Types.Layer.Contracts;
namespace Business.Layer.DataContext
{
    //Bu class sadece örnek oluşturmak için kullanılmıştır.

    //public class CustomDbContext : DbContext
    //{
    //    public DbSet<CommentContract> Products { get; set; }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        // Model yapılandırmasını özelleştirme
    //        modelBuilder.Entity<CommentContract>().ToTable("Yorumlar"); // Tablo adını değiştirme
    //        modelBuilder.Entity<CommentContract>().Property(p => p.Content).HasMaxLength(100); // Alan uzunluğunu sınırlama
    //        modelBuilder.Entity<CommentContract>().HasIndex(p => p.UserId); // İndeks tanımlama
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        // Veritabanı bağlantı ayarlarını yapılandırma
    //       // optionsBuilder.UseSqlServer("connection_string_here");
    //    }

    //    public override int SaveChanges()
    //    {
    //        // Kaydetme işleminden önce veya sonra ek işlemler yapma
    //        // Örneğin, değişiklik tarihlerini otomatik olarak güncelleme
    //        var modifiedEntries = ChangeTracker.Entries()
    //            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    //        foreach (var entry in modifiedEntries)
    //        {
    //            entry.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
    //        }

    //        return base.SaveChanges();
    //    }

    //    //protected override void Dispose(bool disposing)
    //    //{
    //    //    // Kaynakları serbest bırakma
    //    //    // Örneğin, bağlantıyı kapatma
    //    //    if (disposing)
    //    //    {
    //    //        // DbContext kaynaklarını serbest bırak
    //    //        // Örneğin, SqlConnection nesnesini kapat
    //    //    }

    //    //    base.Dispose(disposing);
    //    //}
    //}
}
