using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asm_ph56371.Configurations
{
    public class NguoiDungConfiguration : IEntityTypeConfiguration<NguoiDung>
    {
        public void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.HasKey(g => g.nguoiDungId);


            // Quan hệ 1-1 với bảng GioHang
            builder.HasOne(nd => nd.GioHang)
                .WithOne(g => g.NguoiDung) // Một người dùng có một giỏ hàng
                .HasForeignKey<GioHang>(g => g.NguoiDungId); // Khóa ngoại trong bảng GioHang
        }
    }
}
