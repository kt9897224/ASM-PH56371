using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asm_ph56371.Configurations
{
    public class ChiTietGioHangConfiguration : IEntityTypeConfiguration<ChiTietGioHang>
    {
        public void Configure(EntityTypeBuilder<ChiTietGioHang> builder)
        {
           builder.HasKey(ctgh=>ctgh.CTGHId);

            builder.HasOne(x => x.SanPham)
               .WithMany(x => x.ChiTietGioHangs)
               .HasForeignKey(x => x.SanPhamId);

            builder.HasOne(x => x.GioHang)
               .WithMany(x => x.ChiTietGioHangs)
               .HasForeignKey(x => x.GioHangId  );
        }
    }
}
