using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asm_ph56371.Configurations
{
    public class ChiTietHoaDonConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
        {
            builder.HasKey(cthd=>cthd.CTHDId);

            builder.HasOne(x => x.HoaDon)
                .WithMany(x => x.ChiTietHoaDons)
                .HasForeignKey(x => x.HoaDonId);

            builder.HasOne(x => x.SanPham)
                .WithMany(x => x.chiTietHoaDons)
                .HasForeignKey(x => x.SanPhamId);
        }
    }
}
