using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asm_ph56371.Configurations
{
    public class GioHangConfiguration : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
           builder.HasKey(gh=>gh.gioHangId);

            builder.HasOne(x => x.NguoiDung)
                .WithOne(x => x.GioHang)
                .HasForeignKey<GioHang>(x => x.gioHangId);
        }
    }
}
