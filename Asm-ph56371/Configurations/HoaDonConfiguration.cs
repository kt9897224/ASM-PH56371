using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asm_ph56371.Configurations
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(hd=>hd.hoaDonId);

            builder.HasOne(x => x.NguoiDung)
                .WithMany(x => x.HoaDon)
                .HasForeignKey(x => x.NguoiDungId);
        }
    }
}
