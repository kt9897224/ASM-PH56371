using System.ComponentModel.DataAnnotations;

namespace Asm_ph56371.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public Guid CTGHId { get; set; }  // Khóa chính GUID

        [Required]
        public Guid? GioHangId { get; set; } // Khóa ngoại đến GiỏHàng

        [Required]
        public Guid? SanPhamId { get; set; } // Khóa ngoại đến SảnPhẩm

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; } // Số lượng sản phẩm trong giỏ hàng

        // Quan hệ với GiỏHàng
        public GioHang? GioHang { get; set; }

        // Quan hệ với SảnPhẩm
        public SanPham? SanPham { get; set; }
    }
}
