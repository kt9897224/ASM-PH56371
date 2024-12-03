using Microsoft.EntityFrameworkCore;

namespace Asm_ph56371.Models
{
    public class AsmDbContext : DbContext
    {
        public AsmDbContext()
        {
            
        }

        public AsmDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-8CVDJNS6; Database=ASMC4;Trusted_Connection= True;" +
                                       "TrustServerCertificate=True");
        }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get;set; }
    }
}
